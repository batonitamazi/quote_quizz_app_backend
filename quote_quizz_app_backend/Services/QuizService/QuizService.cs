﻿using quote_quizz_app_backend.Dtos;
using quote_quizz_app_backend.Models;
using quote_quizz_app_backend.Repositories.QuizRepository;

namespace quote_quizz_app_backend.Services.QuizService
{
    public class QuizService : IQuizService
    {
        public readonly IQuizRepository quizRepository;

        public QuizService(IQuizRepository _quizRepository)
        {
            quizRepository = _quizRepository;
        }

        public async Task<IEnumerable<Quiz>> GetQuizList()
        {
            return await quizRepository.GetQuizzes();
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesByTypeAsync(string quizType)
        {
            return await quizRepository.GetQuizzesByTypeAsync(quizType);
        }

        public async Task<QuizDto> GetQuizWithQuestionsAndAnswersAsync(int quizId)
        {
            var data = await quizRepository.GetQuizWithQuestionsAndAnswersAsync(quizId);
            var dto = new QuizDto()
            {
                Id = data.Id,
                Name = data.Name,
                Questions = data.Questions.Select(K => new QuestionDto()
                {
                    Id = K.Id,
                    Question = K.Text,
                    Options = K.Answers.Select(k => k.Text).ToList(),
                    Answer = K.Answers.Where(k => k.IsCorrect).Select(k => k.Text).FirstOrDefault(),
                }).ToList(),
            };
            return dto;
        }

        public async Task SubmitQuizResultAsync(QuizResultDto quizResultDto)
        {

            var quizResult = new QuizResult
            {
                UserId = quizResultDto.UserId,
                QuizId = quizResultDto.QuizId,
                CorrectAnswerCount = quizResultDto.CorrectAnswerCount,
                IncorrectCount = quizResultDto.IncorrectCount,
                TotalQuestions = quizResultDto.TotalQuestions
            };

            await quizRepository.SaveQuizResultAsync(quizResult);
        }

    }
}
