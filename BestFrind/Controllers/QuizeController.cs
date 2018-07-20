using BestFrind.Model;
using BestFrind.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BestFrind.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuizeController : ApiController
    {
        QuizeService _service = new QuizeService();

        /// <summary>
        /// get all quizes
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("api/quize/{userId}")]
        [HttpGet]
        public Response.Response<List<quize>> getAllQuizes(int userId)
        {
            Response.Response<List<quize>> response = new Response.Response<List<quize>>();
            var result = _service.getAllQuizes(userId);
            response.Result = result;
            response.StatusCode = 200;
            return response;
        }

        /// <summary>
        /// get list of quize with count of users answer this quize
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("api/quize/statistic/{userId}")]
        [HttpGet]
        public Response.Response<object> getQuizesStatistic(int userId)
        {
            Response.Response<object> response = new Response.Response<object>();
            var result = _service.getStatistic(userId);
            response.Result = result;
            response.StatusCode = 200;
            return response;
        }


        /// <summary>
        /// get all quizes for userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("api/quize/user/{userId}")]
        [HttpGet]
        public Response.Response<List<quize>> getAllUserQuizes(int userId)
        {
            Response.Response<List<quize>> response = new Response.Response<List<quize>>();
            var result = _service.getAllUserQuizes(userId);
            response.Result = result;
            response.StatusCode = 200;
            return response;
        }

        /// <summary>
        /// get list of quizes with answer for userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("api/quize/question/answer/{userId}")]
        [HttpGet]
        public Response.Response<List<quize>> getUserQuizesWithAnswer(int userId)
        {
            Response.Response<List<quize>> response = new Response.Response<List<quize>>();
            var result = _service.getUserQuizesWithAnswer(userId);
            response.Result = result;
            response.StatusCode = 200;
            return response;
        }

        /// <summary>
        /// create quize for user
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        [Route("api/quize")]
        [HttpPost]
        public Response.Response<int> createQuize(quize obj)
        {
            Response.Response<int> response = new Response.Response<int>();
            //check if quize name is already exist for this user
            bool nameexist = _service.checkQuizeNameExist(obj.userId.Value, obj.name);
            if(!nameexist)
            {
                var result = _service.addQuize(obj);
                response.Result = result;
                response.StatusCode = 200;
            }
            else
            {
                response.Result = 0;
                response.StatusCode = 400;
                response.message = "Name aleardy exist";
            }
            return response;
        }

        /// <summary>
        /// add question to quize
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Route("api/quize/question")]
        [HttpPost]
        public Response.Response<int> addQuizeQuestion(quize_question obj)
        {
            Response.Response<int> response = new Response.Response<int>();
            //check if question already exist in this quize
            bool nameexist = _service.checkQuestionExist(obj.quizeId.Value, obj.question);
            if (!nameexist)
            {
                var result = _service.addQuizeQuestion(obj);
                response.Result = result;
                response.StatusCode = 200;
            }
            else
            {
                response.Result = 0;
                response.StatusCode = 400;
                response.message = "Question aleardy exist";
            }
            return response;
        }

        /// <summary>
        /// answer quize question
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Route("api/quize/question/answer")]
        [HttpPost]
        public Response.Response<int> answerQuizeQuestion(question_answer obj)
        {
            Response.Response<int> response = new Response.Response<int>();
            //check if user is already answer this qestion befor
            bool answered = _service.checkIdQuestionAnswered(obj.questionId.Value, obj.userId.Value);
            //if not answered before add new answer
            if (!answered)
            {
                var result = _service.answerQuizeQuestion(obj);
                response.Result = result;
                response.StatusCode = 200;
            }
            //if answered update existing answer
            else
            {
                var result = _service.updateAnswerQuizeQuestion(obj);
                response.Result = result;
                response.StatusCode = 200;
            }
            return response;
        }

        /// <summary>
        /// check creadintial for user name and password
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Route("api/login")]
        [HttpPost]
        public Response.Response<user> login([FromBody]user obj)
        {
            Response.Response<user> response = new Response.Response<user>();
            var result = _service.login(obj);
            if(result!=null)
            {
                response.Result = result;
                response.StatusCode = 200;
            }
            else
            {
                response.Result = null;
                response.StatusCode =401 ;
            }
            return response;
        }
    }
}
