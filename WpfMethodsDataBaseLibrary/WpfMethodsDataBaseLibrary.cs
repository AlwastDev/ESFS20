using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WpfDataBaseLibrary;

namespace WpfMethodsDataBaseLibrary
{
    public class CrudOperation
    {
        private readonly EsfsContext _context;

        public CrudOperation()
        {
            _context = new EsfsContext();
        }

        /**********************************************************************/
        /*                          INSERT REQUEST                            */
        /**********************************************************************/
        public async Task InsertOperationAccountAsync(string login, string password, string firstname,
            string lastname, string middleName)
        {
            Account account = new Account()
            {
                FirstName = firstname,
                LastName = lastname,
                Login = login,
                Password = password,
                MiddleName = middleName,
                IsAdmin = false,
                GeneralMark = 0
            };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task InsertOperationTheoryAsync(string titleText, string mainText)
        {
            Theory theory = new Theory()
            {
                TitleTheory = titleText,
                TextTheory = mainText
            };
            _context.Theories.Add(theory);
            await _context.SaveChangesAsync();
        }

        public async Task InsertOperationTestAsync(string titleTest)
        {
            Test test = new Test()
            {
                TitleTest = titleTest
            };
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
        }

        public async Task InsertOperationQuestionAsync(string titleQuestion, int testId)
        {
            Question question = new Question()
            {
                TitleQuestion = titleQuestion,
                TestId = testId
            };
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task InsertOperationAnswerAsync(string titleAnswer, int questionId, bool right)
        {
            Answer answer = new Answer()
            {
                TextAnswer = titleAnswer,
                QuestionId = questionId,
                Right = right
            };
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }
        /**********************************************************************/
        /*                          SELECT REQUEST                            */
        /**********************************************************************/

        public async Task<List<Theory>> SelectOperationTheoryAsync()
        {
            return await _context.Theories.ToListAsync();
        }

        public async Task<List<Test>> SelectOperationTestAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        /**********************************************************************/
        /*                          UPDATE REQUEST                            */
        /**********************************************************************/
        public async Task UpdateOperationTheoryAsync(int id, string titleTheory, string textTheory)
        {
            var theory = await _context.Theories.FindAsync(id);
            theory.TitleTheory = titleTheory;
            theory.TextTheory = textTheory;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOperationAnswerAsync(int id, string textAnswer, bool right, int questionId)
        {
            var answer = await _context.Answers.FindAsync(id);
            answer.TextAnswer = textAnswer;
            answer.QuestionId = questionId;
            answer.Right = right;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOperationQuestionAsync(int id, string titleQuestion, int testId)
        {
            var question = await _context.Questions.FindAsync(id);
            question.TitleQuestion = titleQuestion;
            question.TestId = testId;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOperationAccountAsync(int id, int mark)
        {
            var account = await _context.Accounts.FindAsync(id);
            account.GeneralMark += mark;
            await _context.SaveChangesAsync();
        }

        /**********************************************************************/
        /*                          DELETE REQUEST                            */
        /**********************************************************************/
        public async Task DeleteOperationTheoryAsync(int id)
        {
            var theory = await _context.Theories.FindAsync(id);
            _context.Theories.Remove(theory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOperationQuestionAsync(int testId)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.TestId == testId);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOperationTestAsync(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
        }

        /**********************************************************************/
        /*                          Contain REQUEST                           */
        /**********************************************************************/

        public async Task<string?> SelectOperationTheoryTitleContainAsync(int index)
        {
            return await _context.Theories.Where(x => x.Id == index).Select(x => x.TitleTheory).FirstOrDefaultAsync();
        }

        public async Task<string?> SelectOperationTheoryTextContainAsync(int index)
        {
            return await _context.Theories.Where(x => x.Id == index).Select(x => x.TextTheory).FirstOrDefaultAsync();
        }

        public async Task<string?> SelectOperationQuizTitleContainAsync(int index)
        {
            return await _context.Tests.Where(x => x.Id == index).Select(x => x.TitleTest).FirstOrDefaultAsync();
        }

        public async Task<string?> SelectOperationQuestionTitleContainAsync(int index)
        {
            return await _context.Questions.Where(x => x.Id == index).Select(x => x.TitleQuestion)
                .FirstOrDefaultAsync();
        }

        public async Task<List<string?>> SelectOperationAnswerTitleContainAsync(int index)
        {
            return await _context.Answers.Where(x => x.QuestionId == index).Select(x => x.TextAnswer).ToListAsync();
        }

        public async Task<List<bool?>> SelectOperationAnswerRightContainAsync(int index)
        {
            return await _context.Answers.Where(x => x.QuestionId == index).Select(x => x.Right).ToListAsync();
        }

        public async Task<List<Question>> SelectOperationQuestionTestAsync(int index)
        {
            return await _context.Questions.Where(x => x.TestId == index).ToListAsync();
        }


        /**********************************************************************/
        /*                          GET DATA REQUEST                          */
        /**********************************************************************/
        public async Task<string?> GetTrueAnswerAsync(int idQuestion)
        {
            return await _context.Answers.Where(s => s.QuestionId == idQuestion && s.Right == true)
                .Select(x => x.TextAnswer).FirstOrDefaultAsync();
        }

        public async Task<int?> GetMarkAsync(int id)
        {
            return await _context.Accounts.Where(s => s.Id == id).Select(x => x.GeneralMark).FirstAsync();
        }

        public async Task<int> GetIdAccountAsync(string login)
        {
            return await _context.Accounts.Where(x => x.Login == login).Select(x => x.Id).FirstAsync();
        }

        public async Task<IEnumerable<string?>> GetAnswersAsync(int idQuestion)
        {
            return await _context.Answers.Where(s => s.QuestionId == idQuestion).Select(x => x.TextAnswer)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetQuestionsAsync(int idTest)
        {
            return await _context.Questions.Where(s => s.TestId == idTest).Select(x => x.TitleQuestion).ToListAsync();
        }

        public async Task<int> GetQuestionIdAsync(string activeQuestion, int testId)
        {
            return await _context.Questions.Where(s => s.TitleQuestion == activeQuestion && s.TestId == testId)
                .Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<int> GetTestIdAsync(string activeTest)
        {
            return await _context.Tests.Where(s => s.TitleTest == activeTest).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<int> GetAnswerIdAsync(string activeAnswer, int questionId)
        {
            return await _context.Answers.Where(s => s.TextAnswer == activeAnswer && s.QuestionId == questionId)
                .Select(x => x.Id).FirstOrDefaultAsync();
        }

        /**********************************************************************/
        /*                          CHECKERS REQUEST                          */
        /**********************************************************************/
        public async Task<bool> IsAuthAccountAsync(string login, string password)
        {
            return await _context.Accounts.Where(x => x.Login == login && x.Password == password).Select(x => x.Login)
                .ContainsAsync(login);
        }

        public async Task<bool> IsAdminAsync(string login)
        {
            return await _context.Accounts.Where(x => x.Login == login && x.IsAdmin == true).Select(x => x.Login)
                .ContainsAsync(login);
        }

        public async Task<bool> IsLoginAccountAsync(string login)
        {
            return await _context.Accounts.Where(x => x.Login == login).Select(x => x.Login).ContainsAsync(login);
        }

        public async Task<bool> IsCheckTestNameAsync(string activeTest)
        {
            return await _context.Tests.Where(x => x.TitleTest == activeTest).Select(x => x.TitleTest)
                .ContainsAsync(activeTest);
        }

        public async Task<bool> IsCheckQuestionAsync(string titleQuestion, int testId)
        {
            return await _context.Questions.Where(x => x.TitleQuestion == titleQuestion && x.TestId == testId)
                .Select(x => x.TitleQuestion).ContainsAsync(titleQuestion);
        }
    }
}