using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Yaadein.Models;

namespace Yaadein.Data
{
    /// <summary>
    /// Database helper for managing Yaadein data persistence using SQL Server LocalDB
    /// </summary>
    public class DatabaseHelper
    {
        private string connectionString;
        private string databasePath;

        public DatabaseHelper()
        {
            // Set database path to AppData folder
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string yaaadeinFolder = Path.Combine(appDataPath, "Yaadein");

            // Create folder if it doesn't exist
            if (!Directory.Exists(yaaadeinFolder))
            {
                Directory.CreateDirectory(yaaadeinFolder);
            }

            databasePath = Path.Combine(yaaadeinFolder, "YaadeinData.mdf");

            // Connection string for SQL Server LocalDB
            connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;
                                 AttachDbFilename={databasePath};
                                 Integrated Security=True;
                                 Connect Timeout=30";

            InitializeDatabase();
        }

        /// <summary>
        /// Initialize database and create tables if they don't exist
        /// </summary>
        private void InitializeDatabase()
        {
            try
            {
                // Check if database file exists
                if (!File.Exists(databasePath))
                {
                    CreateDatabase();
                }

                CreateTables();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initialize database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Create the database file
        /// </summary>
        private void CreateDatabase()
        {
            string masterConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                             Initial Catalog=master;
                                             Integrated Security=True";

            string createDbQuery = $@"CREATE DATABASE YaadeinData ON PRIMARY 
                                     (NAME = YaadeinData, 
                                      FILENAME = '{databasePath}')";

            using (SqlConnection connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(createDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Create all necessary tables
        /// </summary>
        private void CreateTables()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create People table
                string createPeopleTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'People')
                    CREATE TABLE People (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(200) NOT NULL,
                        Relationship NVARCHAR(100),
                        PhoneNumber NVARCHAR(50),
                        Email NVARCHAR(200),
                        Address NVARCHAR(500),
                        Notes NVARCHAR(MAX),
                        PhotoPath NVARCHAR(500),
                        Birthday DATE,
                        FavoriteMemory NVARCHAR(MAX),
                        ImportantDetails NVARCHAR(MAX),
                        CreatedDate DATETIME DEFAULT GETDATE(),
                        LastContactDate DATETIME,
                        IsFavorite BIT DEFAULT 0,
                        EmergencyContact NVARCHAR(10)
                    )";

                // Create Reminders table
                string createRemindersTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Reminders')
                    CREATE TABLE Reminders (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Title NVARCHAR(200) NOT NULL,
                        Description NVARCHAR(MAX),
                        ReminderTime DATETIME NOT NULL,
                        IsRecurring BIT DEFAULT 0,
                        Recurrence INT DEFAULT 0,
                        IsActive BIT DEFAULT 1,
                        IsCompleted BIT DEFAULT 0,
                        CreatedDate DATETIME DEFAULT GETDATE(),
                        Category NVARCHAR(100),
                        Priority INT DEFAULT 2
                    )";

                // Create Routines table
                string createRoutinesTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Routines')
                    CREATE TABLE Routines (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(200) NOT NULL,
                        Description NVARCHAR(MAX),
                        StartTime TIME NOT NULL,
                        IsActive BIT DEFAULT 1,
                        Category NVARCHAR(100),
                        CreatedDate DATETIME DEFAULT GETDATE(),
                        IconName NVARCHAR(50)
                    )";

                // Create RoutineSteps table
                string createRoutineStepsTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RoutineSteps')
                    CREATE TABLE RoutineSteps (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        RoutineId INT NOT NULL,
                        StepNumber INT NOT NULL,
                        Instruction NVARCHAR(500) NOT NULL,
                        DurationMinutes INT DEFAULT 5,
                        IsCompleted BIT DEFAULT 0,
                        ImagePath NVARCHAR(500),
                        FOREIGN KEY (RoutineId) REFERENCES Routines(Id) ON DELETE CASCADE
                    )";

                // Create MemoryCards table
                string createMemoryCardsTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MemoryCards')
                    CREATE TABLE MemoryCards (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Title NVARCHAR(200) NOT NULL,
                        Content NVARCHAR(MAX),
                        Category NVARCHAR(100),
                        Icon NVARCHAR(10),
                        ImagePath NVARCHAR(500),
                        CreatedDate DATETIME DEFAULT GETDATE()
                    )";

                // Execute table creation commands
                ExecuteNonQuery(createPeopleTable, connection);
                ExecuteNonQuery(createRemindersTable, connection);
                ExecuteNonQuery(createRoutinesTable, connection);
                ExecuteNonQuery(createRoutineStepsTable, connection);
                ExecuteNonQuery(createMemoryCardsTable, connection);
            }
        }

        private void ExecuteNonQuery(string query, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        #region People Operations

        public int AddPerson(Person person)
        {
            string query = @"INSERT INTO People 
                           (Name, Relationship, PhoneNumber, Email, Address, Notes, 
                            PhotoPath, Birthday, FavoriteMemory, ImportantDetails, 
                            CreatedDate, LastContactDate, IsFavorite, EmergencyContact)
                           VALUES 
                           (@Name, @Relationship, @PhoneNumber, @Email, @Address, @Notes,
                            @PhotoPath, @Birthday, @FavoriteMemory, @ImportantDetails,
                            @CreatedDate, @LastContactDate, @IsFavorite, @EmergencyContact);
                           SELECT CAST(SCOPE_IDENTITY() AS INT)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddPersonParameters(command, person);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public void UpdatePerson(Person person)
        {
            string query = @"UPDATE People SET 
                           Name = @Name, Relationship = @Relationship, 
                           PhoneNumber = @PhoneNumber, Email = @Email, 
                           Address = @Address, Notes = @Notes, 
                           PhotoPath = @PhotoPath, Birthday = @Birthday, 
                           FavoriteMemory = @FavoriteMemory, 
                           ImportantDetails = @ImportantDetails,
                           LastContactDate = @LastContactDate, 
                           IsFavorite = @IsFavorite, 
                           EmergencyContact = @EmergencyContact
                           WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", person.Id);
                    AddPersonParameters(command, person);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletePerson(int id)
        {
            string query = "DELETE FROM People WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Person> GetAllPeople()
        {
            List<Person> people = new List<Person>();
            string query = "SELECT * FROM People ORDER BY Name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        people.Add(ReadPerson(reader));
                    }
                }
            }

            return people;
        }

        private void AddPersonParameters(SqlCommand command, Person person)
        {
            command.Parameters.AddWithValue("@Name", person.Name ?? "");
            command.Parameters.AddWithValue("@Relationship", person.Relationship ?? "");
            command.Parameters.AddWithValue("@PhoneNumber", person.PhoneNumber ?? "");
            command.Parameters.AddWithValue("@Email", person.Email ?? "");
            command.Parameters.AddWithValue("@Address", person.Address ?? "");
            command.Parameters.AddWithValue("@Notes", person.Notes ?? "");
            command.Parameters.AddWithValue("@PhotoPath", person.PhotoPath ?? "");
            command.Parameters.AddWithValue("@Birthday", (object)person.Birthday ?? DBNull.Value);
            command.Parameters.AddWithValue("@FavoriteMemory", person.FavoriteMemory ?? "");
            command.Parameters.AddWithValue("@ImportantDetails", person.ImportantDetails ?? "");
            command.Parameters.AddWithValue("@CreatedDate", person.CreatedDate);
            command.Parameters.AddWithValue("@LastContactDate", person.LastContactDate);
            command.Parameters.AddWithValue("@IsFavorite", person.IsFavorite);
            command.Parameters.AddWithValue("@EmergencyContact", person.EmergencyContact ?? "No");
        }

        private Person ReadPerson(SqlDataReader reader)
        {
            return new Person
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Relationship = reader.IsDBNull(2) ? "" : reader.GetString(2),
                PhoneNumber = reader.IsDBNull(3) ? "" : reader.GetString(3),
                Email = reader.IsDBNull(4) ? "" : reader.GetString(4),
                Address = reader.IsDBNull(5) ? "" : reader.GetString(5),
                Notes = reader.IsDBNull(6) ? "" : reader.GetString(6),
                PhotoPath = reader.IsDBNull(7) ? "" : reader.GetString(7),
                Birthday = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                FavoriteMemory = reader.IsDBNull(9) ? "" : reader.GetString(9),
                ImportantDetails = reader.IsDBNull(10) ? "" : reader.GetString(10),
                CreatedDate = reader.GetDateTime(11),
                LastContactDate = reader.GetDateTime(12),
                IsFavorite = reader.GetBoolean(13),
                EmergencyContact = reader.IsDBNull(14) ? "No" : reader.GetString(14)
            };
        }

        #endregion

        #region Reminder Operations

        public int AddReminder(Reminder reminder)
        {
            string query = @"INSERT INTO Reminders 
                           (Title, Description, ReminderTime, IsRecurring, Recurrence,
                            IsActive, IsCompleted, CreatedDate, Category, Priority)
                           VALUES 
                           (@Title, @Description, @ReminderTime, @IsRecurring, @Recurrence,
                            @IsActive, @IsCompleted, @CreatedDate, @Category, @Priority);
                           SELECT CAST(SCOPE_IDENTITY() AS INT)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddReminderParameters(command, reminder);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public void UpdateReminder(Reminder reminder)
        {
            string query = @"UPDATE Reminders SET 
                           Title = @Title, Description = @Description, 
                           ReminderTime = @ReminderTime, IsRecurring = @IsRecurring,
                           Recurrence = @Recurrence, IsActive = @IsActive, 
                           IsCompleted = @IsCompleted, Category = @Category, 
                           Priority = @Priority
                           WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", reminder.Id);
                    AddReminderParameters(command, reminder);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteReminder(int id)
        {
            string query = "DELETE FROM Reminders WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Reminder> GetAllReminders()
        {
            List<Reminder> reminders = new List<Reminder>();
            string query = "SELECT * FROM Reminders ORDER BY ReminderTime";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reminders.Add(ReadReminder(reader));
                    }
                }
            }

            return reminders;
        }

        private void AddReminderParameters(SqlCommand command, Reminder reminder)
        {
            command.Parameters.AddWithValue("@Title", reminder.Title ?? "");
            command.Parameters.AddWithValue("@Description", reminder.Description ?? "");
            command.Parameters.AddWithValue("@ReminderTime", reminder.ReminderTime);
            command.Parameters.AddWithValue("@IsRecurring", reminder.IsRecurring);
            command.Parameters.AddWithValue("@Recurrence", (int)reminder.Recurrence);
            command.Parameters.AddWithValue("@IsActive", reminder.IsActive);
            command.Parameters.AddWithValue("@IsCompleted", reminder.IsCompleted);
            command.Parameters.AddWithValue("@CreatedDate", reminder.CreatedDate);
            command.Parameters.AddWithValue("@Category", reminder.Category ?? "");
            command.Parameters.AddWithValue("@Priority", reminder.Priority);
        }

        private Reminder ReadReminder(SqlDataReader reader)
        {
            return new Reminder
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                ReminderTime = reader.GetDateTime(3),
                IsRecurring = reader.GetBoolean(4),
                Recurrence = (RecurrenceType)reader.GetInt32(5),
                IsActive = reader.GetBoolean(6),
                IsCompleted = reader.GetBoolean(7),
                CreatedDate = reader.GetDateTime(8),
                Category = reader.IsDBNull(9) ? "" : reader.GetString(9),
                Priority = reader.GetInt32(10)
            };
        }

        #endregion

        #region Routine Operations

        public int AddRoutine(Routine routine)
        {
            string query = @"INSERT INTO Routines 
                           (Name, Description, StartTime, IsActive, Category, CreatedDate, IconName)
                           VALUES 
                           (@Name, @Description, @StartTime, @IsActive, @Category, @CreatedDate, @IconName);
                           SELECT CAST(SCOPE_IDENTITY() AS INT)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", routine.Name ?? "");
                    command.Parameters.AddWithValue("@Description", routine.Description ?? "");
                    command.Parameters.AddWithValue("@StartTime", routine.StartTime);
                    command.Parameters.AddWithValue("@IsActive", routine.IsActive);
                    command.Parameters.AddWithValue("@Category", routine.Category ?? "");
                    command.Parameters.AddWithValue("@CreatedDate", routine.CreatedDate);
                    command.Parameters.AddWithValue("@IconName", routine.IconName ?? "");

                    int routineId = (int)command.ExecuteScalar();

                    // Add routine steps
                    foreach (var step in routine.Steps)
                    {
                        AddRoutineStep(routineId, step, connection);
                    }

                    return routineId;
                }
            }
        }

        private void AddRoutineStep(int routineId, RoutineStep step, SqlConnection connection)
        {
            string query = @"INSERT INTO RoutineSteps 
                           (RoutineId, StepNumber, Instruction, DurationMinutes, IsCompleted, ImagePath)
                           VALUES 
                           (@RoutineId, @StepNumber, @Instruction, @DurationMinutes, @IsCompleted, @ImagePath)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RoutineId", routineId);
                command.Parameters.AddWithValue("@StepNumber", step.StepNumber);
                command.Parameters.AddWithValue("@Instruction", step.Instruction ?? "");
                command.Parameters.AddWithValue("@DurationMinutes", step.DurationMinutes);
                command.Parameters.AddWithValue("@IsCompleted", step.IsCompleted);
                command.Parameters.AddWithValue("@ImagePath", step.ImagePath ?? "");
                command.ExecuteNonQuery();
            }
        }

        public void UpdateRoutine(Routine routine)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update routine
                string query = @"UPDATE Routines SET 
                               Name = @Name, Description = @Description, 
                               StartTime = @StartTime, IsActive = @IsActive, 
                               Category = @Category, IconName = @IconName
                               WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", routine.Id);
                    command.Parameters.AddWithValue("@Name", routine.Name ?? "");
                    command.Parameters.AddWithValue("@Description", routine.Description ?? "");
                    command.Parameters.AddWithValue("@StartTime", routine.StartTime);
                    command.Parameters.AddWithValue("@IsActive", routine.IsActive);
                    command.Parameters.AddWithValue("@Category", routine.Category ?? "");
                    command.Parameters.AddWithValue("@IconName", routine.IconName ?? "");
                    command.ExecuteNonQuery();
                }

                // Delete existing steps
                string deleteSteps = "DELETE FROM RoutineSteps WHERE RoutineId = @RoutineId";
                using (SqlCommand command = new SqlCommand(deleteSteps, connection))
                {
                    command.Parameters.AddWithValue("@RoutineId", routine.Id);
                    command.ExecuteNonQuery();
                }

                // Add updated steps
                foreach (var step in routine.Steps)
                {
                    AddRoutineStep(routine.Id, step, connection);
                }
            }
        }

        public void DeleteRoutine(int id)
        {
            string query = "DELETE FROM Routines WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Routine> GetAllRoutines()
        {
            List<Routine> routines = new List<Routine>();
            string query = "SELECT * FROM Routines ORDER BY StartTime";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Routine routine = new Routine
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            StartTime = reader.GetTimeSpan(3),
                            IsActive = reader.GetBoolean(4),
                            Category = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            CreatedDate = reader.GetDateTime(6),
                            IconName = reader.IsDBNull(7) ? "" : reader.GetString(7)
                        };

                        routines.Add(routine);
                    }
                }

                // Load steps for each routine
                foreach (var routine in routines)
                {
                    routine.Steps = GetRoutineSteps(routine.Id, connection);
                }
            }

            return routines;
        }

        private List<RoutineStep> GetRoutineSteps(int routineId, SqlConnection connection)
        {
            List<RoutineStep> steps = new List<RoutineStep>();
            string query = "SELECT * FROM RoutineSteps WHERE RoutineId = @RoutineId ORDER BY StepNumber";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RoutineId", routineId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        steps.Add(new RoutineStep
                        {
                            StepNumber = reader.GetInt32(2),
                            Instruction = reader.GetString(3),
                            DurationMinutes = reader.GetInt32(4),
                            IsCompleted = reader.GetBoolean(5),
                            ImagePath = reader.IsDBNull(6) ? "" : reader.GetString(6)
                        });
                    }
                }
            }

            return steps;
        }

        #endregion

        public void ClearAllData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                ExecuteNonQuery("DELETE FROM RoutineSteps", connection);
                ExecuteNonQuery("DELETE FROM Routines", connection);
                ExecuteNonQuery("DELETE FROM Reminders", connection);
                ExecuteNonQuery("DELETE FROM People", connection);
                ExecuteNonQuery("DELETE FROM MemoryCards", connection);
            }
        }
    }
}