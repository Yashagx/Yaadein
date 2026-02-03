using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Yaadein.Models;

namespace Yaadein.Data
{
    public class DatabaseHelper
    {
        // Set database path to your project's Data folder
        private static string dbPath = @"C:\Users\Win11\OneDrive\Desktop\Yaadein\Data\yaadein.db";
        private static string connectionString = $"Data Source={dbPath};Version=3;";
        public static int CurrentUserId { get; set; }
        public static string CurrentUserName { get; set; }

        public static void InitializeDatabase()
        {
            try
            {
                string directory = Path.GetDirectoryName(dbPath);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                bool needsCreation = !File.Exists(dbPath);

                if (needsCreation)
                {
                    CreateDatabase();
                }
                else
                {
                    // Database file exists, but verify it has tables
                    if (!VerifyDatabase())
                    {
                        // Delete corrupt database and recreate
                        File.Delete(dbPath);
                        CreateDatabase();
                    }
                }

                // Final verification
                if (!VerifyDatabase())
                {
                    throw new Exception("Database verification failed after creation.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Database initialization failed:\n\n{ex.Message}\n\nPath: {dbPath}\n\nStack Trace:\n{ex.StackTrace}",
                    "Database Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                throw;
            }
        }

        private static bool VerifyDatabase()
        {
            try
            {
                if (!File.Exists(dbPath))
                {
                    return false;
                }

                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT name FROM sqlite_master WHERE type='table' AND name='Users'";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        return result != null;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private static void CreateDatabase()
        {
            try
            {
                // Create the database file
                SQLiteConnection.CreateFile(dbPath);

                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Create tables one by one with error handling
                    CreateTable(conn, "Users", @"CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT UNIQUE NOT NULL,
                        PasswordHash TEXT NOT NULL,
                        FullName TEXT NOT NULL,
                        Email TEXT,
                        PhoneNumber TEXT,
                        DateOfBirth TEXT,
                        ProfilePhoto TEXT,
                        IsPatient INTEGER DEFAULT 1,
                        CaregiverUserId INTEGER,
                        CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP,
                        LastLoginDate TEXT
                    )");

                    CreateTable(conn, "Reminders", @"CREATE TABLE IF NOT EXISTS Reminders (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        Title TEXT NOT NULL,
                        Description TEXT,
                        ReminderTime TEXT NOT NULL,
                        IsRecurring INTEGER,
                        Recurrence TEXT,
                        Category TEXT,
                        Priority INTEGER,
                        IsActive INTEGER,
                        IsCompleted INTEGER,
                        CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY(UserId) REFERENCES Users(Id)
                    )");

                    CreateTable(conn, "People", @"CREATE TABLE IF NOT EXISTS People (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        Name TEXT NOT NULL,
                        Relationship TEXT,
                        PhoneNumber TEXT,
                        Email TEXT,
                        Address TEXT,
                        Birthday TEXT,
                        FavoriteMemory TEXT,
                        ImportantDetails TEXT,
                        Notes TEXT,
                        IsFavorite INTEGER,
                        EmergencyContact TEXT,
                        PhotoPath TEXT,
                        CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY(UserId) REFERENCES Users(Id)
                    )");

                    CreateTable(conn, "Routines", @"CREATE TABLE IF NOT EXISTS Routines (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        Name TEXT NOT NULL,
                        Description TEXT,
                        StartTime TEXT,
                        Category TEXT,
                        IsActive INTEGER,
                        CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY(UserId) REFERENCES Users(Id)
                    )");

                    CreateTable(conn, "RoutineSteps", @"CREATE TABLE IF NOT EXISTS RoutineSteps (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        RoutineId INTEGER,
                        StepNumber INTEGER,
                        Instruction TEXT,
                        DurationMinutes INTEGER,
                        FOREIGN KEY(RoutineId) REFERENCES Routines(Id)
                    )");

                    CreateTable(conn, "MemoryJournal", @"CREATE TABLE IF NOT EXISTS MemoryJournal (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        Title TEXT,
                        Content TEXT,
                        EmotionTag TEXT,
                        EmotionIntensity INTEGER,
                        IsVoice INTEGER,
                        AudioPath TEXT,
                        CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY(UserId) REFERENCES Users(Id)
                    )");

                    CreateTable(conn, "EmotionalInsights", @"CREATE TABLE IF NOT EXISTS EmotionalInsights (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        EmotionType TEXT,
                        IntensityLevel INTEGER,
                        TriggerContext TEXT,
                        RecordedDate TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY(UserId) REFERENCES Users(Id)
                    )");

                    CreateTable(conn, "CaregiverAlerts", @"CREATE TABLE IF NOT EXISTS CaregiverAlerts (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        PatientUserId INTEGER NOT NULL,
                        CaregiverUserId INTEGER NOT NULL,
                        AlertType TEXT,
                        Message TEXT,
                        Severity TEXT,
                        IsRead INTEGER DEFAULT 0,
                        CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY(PatientUserId) REFERENCES Users(Id),
                        FOREIGN KEY(CaregiverUserId) REFERENCES Users(Id)
                    )");

                    CreateTable(conn, "MemoryCards", @"CREATE TABLE IF NOT EXISTS MemoryCards (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        Title TEXT NOT NULL,
                        Content TEXT,
                        Category TEXT,
                        Icon TEXT,
                        ImagePath TEXT,
                        CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY(UserId) REFERENCES Users(Id)
                    )");

                    conn.Close();
                }

                System.Windows.Forms.MessageBox.Show(
                    $"Database created successfully!\n\nLocation:\n{dbPath}\n\nYou can now open this file in DB Browser for SQLite.",
                    "Database Created",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create database: {ex.Message}", ex);
            }
        }

        private static void CreateTable(SQLiteConnection conn, string tableName, string createSql)
        {
            try
            {
                using (var cmd = new SQLiteCommand(createSql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create table '{tableName}': {ex.Message}", ex);
            }
        }

        private static void ExecuteNonQuery(SQLiteConnection conn, string sql)
        {
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool ValidateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT Id, FullName FROM Users WHERE Username = @username AND PasswordHash = @password";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CurrentUserId = reader.GetInt32(0);
                            CurrentUserName = reader.GetString(1);

                            reader.Close();

                            string updateLastLogin = "UPDATE Users SET LastLoginDate = @now WHERE Id = @id";
                            using (var updateCmd = new SQLiteCommand(updateLastLogin, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@now", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                updateCmd.Parameters.AddWithValue("@id", CurrentUserId);
                                updateCmd.ExecuteNonQuery();
                            }

                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool RegisterUser(string username, string password, string fullName, string email, bool isPatient)
        {
            string hashedPassword = HashPassword(password);

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Users (Username, PasswordHash, FullName, Email, IsPatient, CreatedDate) 
                                VALUES (@username, @password, @fullname, @email, @isPatient, @created)";

                try
                {
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.Parameters.AddWithValue("@fullname", fullName);
                        cmd.Parameters.AddWithValue("@email", email ?? "");
                        cmd.Parameters.AddWithValue("@isPatient", isPatient ? 1 : 0);
                        cmd.Parameters.AddWithValue("@created", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Registration error: {ex.Message}");
                    System.Windows.Forms.MessageBox.Show(
                        $"Registration failed:\n\n{ex.Message}",
                        "Registration Error",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool UsernameExists(string username)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static string GetDatabasePath()
        {
            return dbPath;
        }

        // ==================== REMINDER METHODS ====================

        public static List<Reminder> GetUserReminders(int userId)
        {
            List<Reminder> reminders = new List<Reminder>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"SELECT Id, Title, Description, ReminderTime, IsRecurring, Recurrence,
                                Category, Priority, IsActive, IsCompleted
                                FROM Reminders WHERE UserId = @userId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reminders.Add(new Reminder
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                ReminderTime = DateTime.Parse(reader.GetString(3)),
                                IsRecurring = reader.GetInt32(4) == 1,
                                Recurrence = (RecurrenceType)Enum.Parse(typeof(RecurrenceType), reader.GetString(5)),
                                Category = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                Priority = reader.GetInt32(7),
                                IsActive = reader.GetInt32(8) == 1,
                                IsCompleted = reader.GetInt32(9) == 1
                            });
                        }
                    }
                }
            }

            return reminders;
        }

        public static void SaveReminder(Reminder reminder, int userId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                if (reminder.Id == 0)
                {
                    // Insert new reminder
                    string insert = @"INSERT INTO Reminders (UserId, Title, Description, ReminderTime, IsRecurring,
                                     Recurrence, Category, Priority, IsActive, IsCompleted)
                                     VALUES (@userId, @title, @description, @reminderTime, @isRecurring,
                                     @recurrence, @category, @priority, @isActive, @isCompleted)";

                    using (var cmd = new SQLiteCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@title", reminder.Title);
                        cmd.Parameters.AddWithValue("@description", reminder.Description ?? "");
                        cmd.Parameters.AddWithValue("@reminderTime", reminder.ReminderTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@isRecurring", reminder.IsRecurring ? 1 : 0);
                        cmd.Parameters.AddWithValue("@recurrence", reminder.Recurrence.ToString());
                        cmd.Parameters.AddWithValue("@category", reminder.Category ?? "");
                        cmd.Parameters.AddWithValue("@priority", reminder.Priority);
                        cmd.Parameters.AddWithValue("@isActive", reminder.IsActive ? 1 : 0);
                        cmd.Parameters.AddWithValue("@isCompleted", reminder.IsCompleted ? 1 : 0);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Update existing reminder
                    string update = @"UPDATE Reminders
                                     SET Title = @title, Description = @description, ReminderTime = @reminderTime,
                                     IsRecurring = @isRecurring, Recurrence = @recurrence, Category = @category,
                                     Priority = @priority, IsActive = @isActive, IsCompleted = @isCompleted
                                     WHERE Id = @id AND UserId = @userId";

                    using (var cmd = new SQLiteCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", reminder.Id);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@title", reminder.Title);
                        cmd.Parameters.AddWithValue("@description", reminder.Description ?? "");
                        cmd.Parameters.AddWithValue("@reminderTime", reminder.ReminderTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@isRecurring", reminder.IsRecurring ? 1 : 0);
                        cmd.Parameters.AddWithValue("@recurrence", reminder.Recurrence.ToString());
                        cmd.Parameters.AddWithValue("@category", reminder.Category ?? "");
                        cmd.Parameters.AddWithValue("@priority", reminder.Priority);
                        cmd.Parameters.AddWithValue("@isActive", reminder.IsActive ? 1 : 0);
                        cmd.Parameters.AddWithValue("@isCompleted", reminder.IsCompleted ? 1 : 0);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void DeleteReminder(int reminderId, int userId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string delete = "DELETE FROM Reminders WHERE Id = @id AND UserId = @userId";

                using (var cmd = new SQLiteCommand(delete, conn))
                {
                    cmd.Parameters.AddWithValue("@id", reminderId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ==================== PEOPLE METHODS ====================

        public static List<Person> GetUserPeople(int userId)
        {
            List<Person> people = new List<Person>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"SELECT Id, Name, Relationship, PhoneNumber, Email, Address, Birthday,
                                FavoriteMemory, ImportantDetails, Notes, IsFavorite, EmergencyContact
                                FROM People WHERE UserId = @userId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            people.Add(new Person
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Relationship = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                PhoneNumber = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Email = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                Address = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                Birthday = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                FavoriteMemory = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                ImportantDetails = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                Notes = reader.IsDBNull(9) ? "" : reader.GetString(9),
                                IsFavorite = reader.GetInt32(10) == 1,
                                EmergencyContact = reader.IsDBNull(11) ? "No" : reader.GetString(11)
                            });
                        }
                    }
                }
            }

            return people;
        }

        public static void SavePerson(Person person, int userId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                if (person.Id == 0)
                {
                    // Insert new person
                    string insert = @"INSERT INTO People (UserId, Name, Relationship, PhoneNumber, Email, Address,
                                     Birthday, FavoriteMemory, ImportantDetails, Notes, IsFavorite, EmergencyContact)
                                     VALUES (@userId, @name, @relationship, @phone, @email, @address,
                                     @birthday, @favoriteMemory, @importantDetails, @notes, @isFavorite, @emergencyContact)";

                    using (var cmd = new SQLiteCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@name", person.Name);
                        cmd.Parameters.AddWithValue("@relationship", person.Relationship ?? "");
                        cmd.Parameters.AddWithValue("@phone", person.PhoneNumber ?? "");
                        cmd.Parameters.AddWithValue("@email", person.Email ?? "");
                        cmd.Parameters.AddWithValue("@address", person.Address ?? "");
                        cmd.Parameters.AddWithValue("@birthday", person.Birthday.HasValue ? person.Birthday.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@favoriteMemory", person.FavoriteMemory ?? "");
                        cmd.Parameters.AddWithValue("@importantDetails", person.ImportantDetails ?? "");
                        cmd.Parameters.AddWithValue("@notes", person.Notes ?? "");
                        cmd.Parameters.AddWithValue("@isFavorite", person.IsFavorite ? 1 : 0);
                        cmd.Parameters.AddWithValue("@emergencyContact", person.EmergencyContact ?? "No");
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Update existing person
                    string update = @"UPDATE People
                                     SET Name = @name, Relationship = @relationship, PhoneNumber = @phone,
                                     Email = @email, Address = @address, Birthday = @birthday,
                                     FavoriteMemory = @favoriteMemory, ImportantDetails = @importantDetails,
                                     Notes = @notes, IsFavorite = @isFavorite, EmergencyContact = @emergencyContact
                                     WHERE Id = @id AND UserId = @userId";

                    using (var cmd = new SQLiteCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", person.Id);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@name", person.Name);
                        cmd.Parameters.AddWithValue("@relationship", person.Relationship ?? "");
                        cmd.Parameters.AddWithValue("@phone", person.PhoneNumber ?? "");
                        cmd.Parameters.AddWithValue("@email", person.Email ?? "");
                        cmd.Parameters.AddWithValue("@address", person.Address ?? "");
                        cmd.Parameters.AddWithValue("@birthday", person.Birthday.HasValue ? person.Birthday.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@favoriteMemory", person.FavoriteMemory ?? "");
                        cmd.Parameters.AddWithValue("@importantDetails", person.ImportantDetails ?? "");
                        cmd.Parameters.AddWithValue("@notes", person.Notes ?? "");
                        cmd.Parameters.AddWithValue("@isFavorite", person.IsFavorite ? 1 : 0);
                        cmd.Parameters.AddWithValue("@emergencyContact", person.EmergencyContact ?? "No");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void DeletePerson(int personId, int userId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string delete = "DELETE FROM People WHERE Id = @id AND UserId = @userId";

                using (var cmd = new SQLiteCommand(delete, conn))
                {
                    cmd.Parameters.AddWithValue("@id", personId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ==================== ROUTINE METHODS ====================

        public static List<Routine> GetUserRoutines(int userId)
        {
            List<Routine> routines = new List<Routine>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"SELECT Id, Name, Description, StartTime, Category, IsActive
                                FROM Routines WHERE UserId = @userId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var routine = new Routine
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                StartTime = TimeSpan.Parse(reader.GetString(3)),
                                Category = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                IsActive = reader.GetInt32(5) == 1,
                                Steps = new List<RoutineStep>()
                            };

                            // Load steps for this routine
                            routine.Steps = GetRoutineSteps(conn, routine.Id);
                            routines.Add(routine);
                        }
                    }
                }
            }

            return routines;
        }

        private static List<RoutineStep> GetRoutineSteps(SQLiteConnection conn, int routineId)
        {
            List<RoutineStep> steps = new List<RoutineStep>();

            string query = @"SELECT StepNumber, Instruction, DurationMinutes
                            FROM RoutineSteps WHERE RoutineId = @routineId ORDER BY StepNumber";

            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@routineId", routineId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        steps.Add(new RoutineStep
                        {
                            StepNumber = reader.GetInt32(0),
                            Instruction = reader.GetString(1),
                            DurationMinutes = reader.GetInt32(2)
                        });
                    }
                }
            }

            return steps;
        }

        public static void SaveRoutine(Routine routine, int userId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        if (routine.Id == 0)
                        {
                            // Insert new routine
                            string insert = @"INSERT INTO Routines (UserId, Name, Description, StartTime, Category, IsActive)
                                             VALUES (@userId, @name, @description, @startTime, @category, @isActive);
                                             SELECT last_insert_rowid();";

                            using (var cmd = new SQLiteCommand(insert, conn))
                            {
                                cmd.Parameters.AddWithValue("@userId", userId);
                                cmd.Parameters.AddWithValue("@name", routine.Name);
                                cmd.Parameters.AddWithValue("@description", routine.Description ?? "");
                                cmd.Parameters.AddWithValue("@startTime", routine.StartTime.ToString());
                                cmd.Parameters.AddWithValue("@category", routine.Category ?? "");
                                cmd.Parameters.AddWithValue("@isActive", routine.IsActive ? 1 : 0);

                                routine.Id = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                        }
                        else
                        {
                            // Update existing routine
                            string update = @"UPDATE Routines
                                             SET Name = @name, Description = @description, StartTime = @startTime,
                                             Category = @category, IsActive = @isActive
                                             WHERE Id = @id AND UserId = @userId";

                            using (var cmd = new SQLiteCommand(update, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", routine.Id);
                                cmd.Parameters.AddWithValue("@userId", userId);
                                cmd.Parameters.AddWithValue("@name", routine.Name);
                                cmd.Parameters.AddWithValue("@description", routine.Description ?? "");
                                cmd.Parameters.AddWithValue("@startTime", routine.StartTime.ToString());
                                cmd.Parameters.AddWithValue("@category", routine.Category ?? "");
                                cmd.Parameters.AddWithValue("@isActive", routine.IsActive ? 1 : 0);
                                cmd.ExecuteNonQuery();
                            }

                            // Delete existing steps
                            string deleteSteps = "DELETE FROM RoutineSteps WHERE RoutineId = @routineId";
                            using (var cmd = new SQLiteCommand(deleteSteps, conn))
                            {
                                cmd.Parameters.AddWithValue("@routineId", routine.Id);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Insert steps
                        foreach (var step in routine.Steps)
                        {
                            string insertStep = @"INSERT INTO RoutineSteps (RoutineId, StepNumber, Instruction, DurationMinutes)
                                                 VALUES (@routineId, @stepNumber, @instruction, @duration)";

                            using (var cmd = new SQLiteCommand(insertStep, conn))
                            {
                                cmd.Parameters.AddWithValue("@routineId", routine.Id);
                                cmd.Parameters.AddWithValue("@stepNumber", step.StepNumber);
                                cmd.Parameters.AddWithValue("@instruction", step.Instruction);
                                cmd.Parameters.AddWithValue("@duration", step.DurationMinutes);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void DeleteRoutine(int routineId, int userId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Delete steps first
                        string deleteSteps = "DELETE FROM RoutineSteps WHERE RoutineId = @routineId";
                        using (var cmd = new SQLiteCommand(deleteSteps, conn))
                        {
                            cmd.Parameters.AddWithValue("@routineId", routineId);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete routine
                        string deleteRoutine = "DELETE FROM Routines WHERE Id = @id AND UserId = @userId";
                        using (var cmd = new SQLiteCommand(deleteRoutine, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", routineId);
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Emotional State Logging for AI Companion
        public static void LogEmotionalState(int userId, string emotion, int intensity, string context, DateTime timestamp)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO EmotionalInsights 
                                (UserId, EmotionType, IntensityLevel, TriggerContext, RecordedDate)
                                VALUES (@userId, @emotion, @intensity, @context, @timestamp)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@emotion", emotion);
                    cmd.Parameters.AddWithValue("@intensity", intensity);
                    cmd.Parameters.AddWithValue("@context", context ?? "");
                    cmd.Parameters.AddWithValue("@timestamp", timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<EmotionalLog> GetEmotionalLogs(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<EmotionalLog> logs = new List<EmotionalLog>();

            using (var conn = GetConnection())
            {
                conn.Open();

                string query = @"SELECT Id, EmotionType, IntensityLevel, TriggerContext, RecordedDate
                                FROM EmotionalInsights 
                                WHERE UserId = @userId";

                if (startDate.HasValue)
                    query += " AND RecordedDate >= @startDate";
                if (endDate.HasValue)
                    query += " AND RecordedDate <= @endDate";

                query += " ORDER BY RecordedDate DESC LIMIT 100";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    if (startDate.HasValue)
                        cmd.Parameters.AddWithValue("@startDate", startDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (endDate.HasValue)
                        cmd.Parameters.AddWithValue("@endDate", endDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new EmotionalLog
                            {
                                Id = reader.GetInt32(0),
                                Emotion = reader.GetString(1),
                                Intensity = reader.GetInt32(2),
                                Context = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Timestamp = DateTime.Parse(reader.GetString(4))
                            });
                        }
                    }
                }
            }

            return logs;
        }
    }

    // Emotional Log Model
    public class EmotionalLog
    {
        public int Id { get; set; }
        public string Emotion { get; set; }
        public int Intensity { get; set; }
        public string Context { get; set; }
        public DateTime Timestamp { get; set; }
    }
}