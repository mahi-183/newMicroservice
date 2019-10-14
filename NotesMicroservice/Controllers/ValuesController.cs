// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValuesController.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace NotesMicroservice.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value28" };
        }

        [HttpGet]
        [Route("getUserDetails/{id}")]
        public async Task<IList<ApplicationUserModel>> VerirfyUser(string id)
        {
            try
            {
                IList<ApplicationUserModel> product = null;
                using (var client = new HttpClient())
                {
                    //// gets the token which is passed
                    var accessToken = Request.Headers["Authorization"].ToString();
                    string[] strArr = null;
                    char[] splitchar = { ' ' };

                    //// splits the token to get the token without bearer keyword
                    strArr = accessToken.Split(splitchar);

                    //// adding token in httpclient instance
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strArr[1]);

                    //// calling api from other services
                    var response = await client.GetAsync("https://localhost:44330/api/AccountUser/GetUser?userId=" + id);
                    response.EnsureSuccessStatusCode();
                    product = await response.Content.ReadAsAsync<IList<ApplicationUserModel>>();
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get the notes details by user id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>return result.</returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
