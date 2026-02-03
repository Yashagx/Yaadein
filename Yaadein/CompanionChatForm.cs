using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Yaadein
{
    public class CompanionChatForm : Form
    {
        private Panel panelTop;
        private Label lblTitle;
        private RichTextBox rtbChat;
        private Panel panelBottom;
        private TextBox txtInput;
        private Button btnSend;
        private Button btnClose;

        private const string WATERMARK_TEXT = "Type a message…";
        private bool _watermarkVisible = false;
        private bool _isProcessing = false;

        private static readonly HttpClient httpClient = new HttpClient();
        private const string GROQ_API_KEY = "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";
        private const string GROQ_API_URL = "https://api.groq.com/openai/v1/chat/completions";

        public CompanionChatForm()
        {
            InitializeComponents();
            ShowWatermark();
            AppendMessage("Companion", "Hi! I'm your Yaadein Companion. 🤗 I'm here to chat whenever you need. How are you doing today?");

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {GROQ_API_KEY}");
        }

        private void InitializeComponents()
        {
            this.Text = "Yaadein – Companion Chat";
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(500, 600);
            this.BackColor = Color.FromArgb(250, 250, 250);
            this.Font = new Font("Segoe UI", 10F);
            this.FormBorderStyle = FormBorderStyle.Sizable;

            panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(255, 192, 203)
            };

            lblTitle = new Label
            {
                Text = "💬  AI Companion Chat",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                AutoSize = false,
                Width = 450
            };

            btnClose = new Button
            {
                Text = "✕",
                Dock = DockStyle.Right,
                Width = 60,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(255, 105, 180),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(btnClose);

            rtbChat = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(60, 60, 60),
                Font = new Font("Segoe UI", 11F),
                ScrollBars = RichTextBoxScrollBars.Vertical,
                WordWrap = true,
                Padding = new Padding(15)
            };

            panelBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(255, 240, 245),
                Padding = new Padding(15)
            };

            txtInput = new TextBox
            {
                Multiline = true,
                Font = new Font("Segoe UI", 11F),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill
            };

            btnSend = new Button
            {
                Text = "Send",
                Dock = DockStyle.Right,
                Width = 100,
                BackColor = Color.FromArgb(255, 182, 193),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSend.FlatAppearance.BorderSize = 0;

            panelBottom.Controls.Add(txtInput);
            panelBottom.Controls.Add(btnSend);

            this.Controls.Add(rtbChat);
            this.Controls.Add(panelTop);
            this.Controls.Add(panelBottom);

            btnSend.Click += btnSend_Click;
            txtInput.KeyDown += txtInput_KeyDown;
            txtInput.GotFocus += txtInput_GotFocus;
            txtInput.LostFocus += txtInput_LostFocus;
        }

        private void ShowWatermark()
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                txtInput.Text = WATERMARK_TEXT;
                txtInput.ForeColor = Color.FromArgb(160, 160, 160);
                _watermarkVisible = true;
            }
        }

        private void HideWatermark()
        {
            if (_watermarkVisible)
            {
                txtInput.Text = "";
                txtInput.ForeColor = Color.FromArgb(60, 60, 60);
                _watermarkVisible = false;
            }
        }

        private void txtInput_GotFocus(object sender, EventArgs e)
        {
            HideWatermark();
        }

        private void txtInput_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
                ShowWatermark();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMessage();
        }

        private async void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && !e.Shift)
            {
                e.SuppressKeyPress = true;
                await SendMessage();
            }
        }

        private async Task SendMessage()
        {
            if (_isProcessing) return;

            string userText = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(userText) || _watermarkVisible)
                return;

            _isProcessing = true;
            btnSend.Enabled = false;
            btnSend.Text = "...";

            AppendMessage("You", userText);

            txtInput.Text = "";
            ShowWatermark();

            string reply = await GetAIResponse(userText);
            AppendMessage("Companion", reply);

            btnSend.Enabled = true;
            btnSend.Text = "Send";
            _isProcessing = false;
            txtInput.Focus();
        }

        private async Task<string> GetAIResponse(string userMessage)
        {
            try
            {
                var requestBody = new
                {
                    model = "mixtral-8x7b-32768",
                    messages = new[]
                    {
                        new
                        {
                            role = "system",
                            content = "You are a warm, compassionate AI companion for someone with memory challenges (Alzheimer's). Your responses should be: 1) Kind, patient, and encouraging 2) Simple and easy to understand (2-4 sentences) 3) Supportive and reassuring 4) Conversational and natural, not robotic 5) Empathetic to their feelings. Never mention that you're an AI. Speak like a caring friend."
                        },
                        new
                        {
                            role = "user",
                            content = userMessage
                        }
                    },
                    temperature = 0.8,
                    max_tokens = 200
                };

                string jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(GROQ_API_URL, content);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return "I'm having a little trouble right now, but I'm still here with you. How about we talk about something that makes you happy? 😊";
                }

                var jsonResponse = JObject.Parse(responseBody);
                string aiMessage = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString();

                return aiMessage ?? "I'm here with you. Tell me more about what's on your mind. 💛";
            }
            catch (Exception)
            {
                return "I'm having a moment of difficulty, but I'm still here listening. What would you like to talk about? 🌸";
            }
        }

        private void AppendMessage(string sender, string text)
        {
            bool isUser = (sender == "You");

            string label = isUser ? "You" : "🤖 Companion";
            Color labelColor = isUser ? Color.FromArgb(255, 105, 180) : Color.FromArgb(80, 80, 80);
            Color bubbleColor = isUser ? Color.FromArgb(255, 240, 245) : Color.FromArgb(245, 245, 245);

            rtbChat.SelectionStart = rtbChat.Text.Length;
            rtbChat.SelectionLength = 0;

            rtbChat.AppendText("\n");

            int startIndex = rtbChat.Text.Length;
            rtbChat.AppendText(label + "\n");
            rtbChat.SelectionStart = startIndex;
            rtbChat.SelectionLength = label.Length;
            rtbChat.SelectionColor = labelColor;
            rtbChat.SelectionFont = new Font("Segoe UI", 9F, FontStyle.Bold);

            startIndex = rtbChat.Text.Length;
            rtbChat.AppendText(text + "\n");
            rtbChat.SelectionStart = startIndex;
            rtbChat.SelectionLength = text.Length;
            rtbChat.SelectionColor = Color.FromArgb(40, 40, 40);
            rtbChat.SelectionFont = new Font("Segoe UI", 11F);
            rtbChat.SelectionBackColor = bubbleColor;

            rtbChat.SelectionStart = rtbChat.Text.Length;
            rtbChat.ScrollToCaret();
        }
    }
}