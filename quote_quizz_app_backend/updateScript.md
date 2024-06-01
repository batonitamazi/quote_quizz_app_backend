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
