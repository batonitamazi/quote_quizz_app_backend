IF DB_ID('QuotesAppDb') IS NULL
BEGIN
    CREATE DATABASE QuotesAppDb;
END;
GO


IF OBJECT_ID('dbo.Users', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL,
        PasswordHash NVARCHAR(64) NOT NULL
    );
END;
GO

IF OBJECT_ID('dbo.Quizzes', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Quizzes
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL
    );
END;
GO

IF OBJECT_ID('dbo.Questions', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Questions
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Text NVARCHAR(MAX) NOT NULL,
        QuizId INT NOT NULL,
        CONSTRAINT FK_Questions_QuizId FOREIGN KEY (QuizId)
        REFERENCES dbo.Quizzes(Id) ON DELETE CASCADE
    );
END;
GO
IF OBJECT_ID('dbo.Answers', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Answers
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Text NVARCHAR(MAX) NOT NULL,
        QuestionId INT NOT NULL,
        CONSTRAINT FK_Answers_QuestionId FOREIGN KEY (QuestionId)
        REFERENCES dbo.Questions(Id) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Quizzes' AND COLUMN_NAME = 'Type')
BEGIN
    ALTER TABLE dbo.Quizzes
    ADD Type NVARCHAR(100) NOT NULL;
END;
GO

ALTER TABLE Answers ADD IsCorrect BIT NOT NULL DEFAULT 0;


IF OBJECT_ID('dbo.QuizResults', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.QuizResults
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        QuizId INT NOT NULL,
        CorrectAnswerCount INT NOT NULL,
        IncorrectCount INT NOT NULL,
        TotalQuestions INT NOT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE()
    );

    ALTER TABLE dbo.QuizResults
    ADD CONSTRAINT FK_QuizResults_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id),
        CONSTRAINT FK_QuizResults_Quizzes FOREIGN KEY (QuizId) REFERENCES dbo.Quizzes(Id);
END;
GO