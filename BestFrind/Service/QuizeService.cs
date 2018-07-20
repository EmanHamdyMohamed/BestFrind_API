using BestFrind.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestFrind.Service
{
    public class QuizeService
    {
        bestFrindEntities context = new bestFrindEntities();

        /// <summary>
        /// get all quizes for all user except this user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<quize> getAllQuizes(int userId)
        {
            var result = context.quizes.Where(q => q.userId != userId);
            return result.ToList();
        }

        /// <summary>
        /// get all quizes for this user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<quize> getAllUserQuizes(int userId)
        {
            var result = context.quizes.Where(q => q.userId == userId);
            return result.ToList();
        }

        /// <summary>
        /// get list of quizes with its answers
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<quize> getUserQuizesWithAnswer(int userId)
        {
            var result = context.quizes.Where(q => q.userId != userId);
            return result.ToList();
        }

        /// <summary>
        /// add quize
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int addQuize(quize obj)
        {
             context.quizes.Add(obj);
            context.SaveChanges();
            return obj.Id;
        }

        /// <summary>
        /// add question to quize
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int addQuizeQuestion(quize_question obj)
        {
            context.quize_question.Add(obj);
            context.SaveChanges();
            return obj.Id;
        }
        /// <summary>
        /// aswer question
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int answerQuizeQuestion(question_answer obj)
        {
            context.question_answer.Add(obj);
            context.SaveChanges();
            return obj.Id;
        }

        /// <summary>
        /// update question answer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int updateAnswerQuizeQuestion(question_answer obj)
        {
            var data = context.question_answer.Where(q => q.userId == obj.userId && q.questionId == obj.questionId).FirstOrDefault();
            data.answer = obj.answer;
            //context.question_answer.Add(obj);
            context.SaveChanges();
            return obj.Id;
        }

        /// <summary>
        /// user login
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public user login(user obj)
        {

            var result=context.users.Where(u=>u.username==obj.username && u.password==obj.password).FirstOrDefault();
            if(result!=null)
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// check if quize name already exist for this user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool checkQuizeNameExist(int userId,string name)
        {
            var result = context.quizes.Where(q => q.userId == userId && q.name == name).ToList();
            if(result.Count > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
            
        }

        /// <summary>
        /// check if question is already exist in quize
        /// </summary>
        /// <param name="quizeId"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        public bool checkQuestionExist(int quizeId, string question)
        {
            var result = context.quize_question.Where(q => q.quizeId == quizeId && q.question == question).ToList();
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// check if user is already answer this question before
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool checkIdQuestionAnswered(int questionId, int userId)
        {
            var result = context.question_answer.Where(q => q.questionId == questionId && q.userId == userId).ToList();
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// get list of quizes with count of user who answer this quizes
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object getStatistic(int userId)
        {
            var result =( from q in context.question_answer
                         where q.userId != userId
                         group q by (q.quize_question.quizeId) into g
                         select new { count = g.Select(a=>a.userId).Distinct().Count(), quizeId = g.Key,quize=g.Select(w=>w.quize_question.quize.name).Distinct().FirstOrDefault(), user=g.Select(u=>u.user.name).Distinct().FirstOrDefault() });

            return result;

        }
    }
}