using CollegeApp.Model;
using CollegeApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.DTO;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.AspNetCore.JsonPatch;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;


using CollegeApp.Service;

namespace CollegeApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(PolicyName = "LocalHost")]
    [Authorize(AuthenticationSchemes = "LocalJwt", Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
       
        private readonly IMapper _mapper;

       
        private readonly ICollegeRepository<Student> _collegeRepository;
        private ApiResponse apiResponse;
        private ICacheProvider _cacheProvider;
        public StudentController(ILogger<StudentController> logger, IMapper mapper, ICollegeRepository<Student> collegeRepository, ICacheProvider cacheProvider)
        {
            _logger = logger;
            _cacheProvider = cacheProvider;
            _mapper = mapper;
           
            _collegeRepository = collegeRepository;
            apiResponse = new ApiResponse();
        }

        [HttpGet("GetStudentName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetStudentName()
        {

            //var StudentsDTO =  new List<StudentDTO>();
            //foreach (var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO();
            //    {
            //        obj.Id = item.Id;
            //        obj.Name = item.StudentName;
            //        obj.Address = item.Address;
            //        obj.Email = item.Email;
            //        obj.DOB = item.DOB;

            //    };
            //    StudentsDTO.Add(obj);
            //}
            try
            {
                _logger.LogInformation("Method has Started");
                var stu = await _cacheProvider.GetCachedResponse();
                //var stu = await _collegeRepository.GetAll();
                apiResponse.data = _mapper.Map<List<StudentDTO>>(stu);
                apiResponse.Status = true;
                apiResponse.StatusCode = HttpStatusCode.OK;
                //var StuData = await _dbContext.Students.Select(s => new StudentDTO
                //{
                //    Id = s.Id,
                //    Name = s.StudentName,
                //    Address = s.Address,
                //    Email = s.Email,
                //    DOB = s.DOB
                //}).ToListAsync();
                _logger.LogInformation("Success");
                return Ok(apiResponse);
            }
            catch (Exception ex) {

                apiResponse.Status = false;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Errors.Add(ex.Message);
                return apiResponse;
            }

            
        }

        [HttpGet("{id:int}", Name = "GetStudentNamebyId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetStudentNamebyId(int id)
        {
           
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                var stu = await _collegeRepository.GetbyId(x => x.Id == id);

                if (stu == null)
                {

                    return NotFound($"The student with id {id} not found");
                }
                apiResponse.data = _mapper.Map<StudentDTO>(stu);
                apiResponse.Status = true;
                apiResponse.StatusCode = HttpStatusCode.OK;
                //var studentData = new StudentDTO()
                //{
                //    Id = stu.Id,
                //    Name = stu.StudentName,
                //    Address = stu.Address,
                //    Email = stu.Email,
                //    DOB = stu.DOB
                //};
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {

                apiResponse.Status = false;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Errors.Add(ex.Message);
                return apiResponse;
            }
        }

        [HttpGet("GetStudentNamebyName/{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetStudentNamebyName(string name)
        {
           
            try
            {
                if (name == "")
                {
                    return BadRequest();
                }
                var stu = await _collegeRepository.GetbyName(x => x.StudentName.ToLower().Contains(name.ToLower()));
                if (stu == null)
                {
                    return NotFound($"The student with name {name} not found");
                }
                apiResponse.data = _mapper.Map<StudentDTO>(stu);
                apiResponse.Status = true;
                apiResponse.StatusCode = HttpStatusCode.OK;
                //var studentData = new StudentDTO()
                //{
                //    Id = stu.Id,
                //    Name = stu.StudentName,
                //    Address = stu.Address,
                //    Email = stu.Email,
                //    DOB = stu.DOB
                //};
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {

                apiResponse.Status = false;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Errors.Add(ex.Message);
                return apiResponse;
            }
        }

        [HttpDelete("RemoveStudentById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> RemoveStudentById(int id)
        {
           
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var stu = await _collegeRepository.GetbyId(x => x.Id == id);
                if (stu == null)
                {
                    return NotFound($"The student with name {id} not found");
                }

                apiResponse.data = await _collegeRepository.DeleteData(stu);
                apiResponse.Status = true;
                apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {

                apiResponse.Status = false;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Errors.Add(ex.Message);
                return apiResponse;
            }
        }

        [HttpPost("CreateStudentData")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateStudentData([FromBody] StudentDTO dto)
        {
           
            try
            {
                // Manual validation
                //if (!ModelState.IsValid) { return BadRequest(ModelState); }

                if (dto == null)

                { return BadRequest(); }

                if (dto.Address.Length < 3)
                {
                    ModelState.AddModelError("ErroDetails", "Address should be more than 3 characters");
                    return BadRequest(ModelState);
                }


                var student = _mapper.Map<Student>(dto);

                var createdStudent = await _collegeRepository.CreateData(student);
                //Student student =  new Student() { 

                //    StudentName = dto.Name,
                //    Address = dto.Address,
                //    Email = dto.Email,
                //    DOB = dto.DOB

                //};

                dto.Id = createdStudent.Id;
                apiResponse.data = dto;
                apiResponse.Status = true;
                apiResponse.StatusCode = HttpStatusCode.Created;
                // return CreatedAtRoute("GetStudentNamebyId", new { id = dto.Id}, dto);
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {

                apiResponse.Status = false;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Errors.Add(ex.Message);
                return apiResponse;
            }
        }


        [HttpPut("UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudent(StudentDTO dto) 
        
        {
            if (dto == null || dto.Id <= 0)

            { return BadRequest(); }

          //  var existingStudent = _dbContext.Students.Where(s => s.Id == dto.Id).FirstOrDefault();
            var existingStudentasnoTacking = await _collegeRepository.GetbyId(x => x.Id == dto.Id, true);
            if (existingStudentasnoTacking == null) { 
            return NotFound();
            }

            if (dto.Address.Length < 3)
            {
                ModelState.AddModelError("ErroDetails", "Address should be more than 3 characters");
                return BadRequest(ModelState);
            }
            var StudData = _mapper.Map<Student>(existingStudentasnoTacking);
            //var StudData = new Student()
            //{
            //    Id = existingStudentasnoTacking.Id,
            //    StudentName = existingStudentasnoTacking.StudentName,
            //    Email = existingStudentasnoTacking.Email,
            //    Address = existingStudentasnoTacking.Address,
            //    DOB = existingStudentasnoTacking.DOB
            //};


            //existingStudent.Address = dto.Address;
            //existingStudent.Email = dto.Email;  
            //existingStudent.DOB = dto.DOB;
            //existingStudent.StudentName = dto.Name;

            await _collegeRepository.UpdateData(StudData);
            return NoContent();
        }

        [HttpPatch("UpdatePatch")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)

            { return BadRequest(); }
            var existingStudent = await _collegeRepository.GetbyId(x => x.Id == id, true);
            if (existingStudent == null)
            {
                return NotFound();
            }

            var StudentDTOData = _mapper.Map<StudentDTO>(existingStudent);
            //var StudentDTOData = new StudentDTO()
            //{
            //    Id = existingStudent.Id,
            //    Name = existingStudent.StudentName,
            //    Email = existingStudent.Email,
            //    DOB = existingStudent.DOB,
            //    Address = existingStudent.Address

            //};
            patchDocument.ApplyTo(StudentDTOData, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //existingStudent.Id = StudentDTOData.Id;
            //existingStudent.Address = StudentDTOData.Address;
            //existingStudent.Email = StudentDTOData.Email;
            //existingStudent.DOB = StudentDTOData.DOB;
            //existingStudent.StudentName = StudentDTOData.Name;
            existingStudent = _mapper.Map<Student>(StudentDTOData);
            await _collegeRepository.UpdateData(existingStudent);
            return NoContent();
        }
        




    }
}
