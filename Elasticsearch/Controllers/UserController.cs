using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Elasticsearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;

        public UserController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _elasticClient.SearchAsync<User>(
                q => q.Query(qu => qu.MatchAll()));
            return Ok(res.Documents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _elasticClient.GetAsync<User>(id);
            return Ok(res.Source);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var res = await _elasticClient.UpdateAsync<User>(user.Id, d => d.Doc(user));
            return Ok(res.IsValid);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var res = await _elasticClient.IndexAsync(user, i => i.Index("index"));
            return Ok(res.IsValid);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _elasticClient.DeleteAsync<User>(id);
            return Ok(res.IsValid);
        }
    }
}
