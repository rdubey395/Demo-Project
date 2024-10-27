using AutoMapper;
using CollegeApp.DTO;
using CollegeApp.Model;
using CollegeApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePrivilegesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICollegeRepository<RolePrivilege> _collegeRepository;
        private ApiResponse _response;
        public RolePrivilegesController(IMapper mapper, ICollegeRepository<RolePrivilege> collegeRepository)
        {
            _mapper = mapper;
            _response = new ApiResponse();
            _collegeRepository = collegeRepository;
        }


        [HttpPost("CreateRolePrivilege")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateRolePrivilege(RolePrivilegeDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Please provide Role");
                }

                var data = _mapper.Map<RolePrivilege>(dto);
                data.IsDeleted = false;
                data.CreatedDate = DateTime.Now;

                await _collegeRepository.CreateData(data);
                dto.Id = data.Id;
                _response.data = data;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Status = true;
                return _response;
            }
            catch (Exception ex)
            {
                {
                    _response.Errors.Add(ex.Message);
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.Status = false;
                    return _response;
                }



            }
        }

        [HttpGet("GetAllRolePrivilege")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetAllRolePrivilege()
        {
            try
            {

                var data = await _collegeRepository.GetAll();

                if (data == null)
                {
                    return BadRequest("No data Available");
                }

                _response.data = _mapper.Map<List<RolePrivilegeDTO>>(data);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Status = true;
                return _response;
            }
            catch (Exception ex)
            {
                {
                    _response.Errors.Add(ex.Message);
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.Status = false;
                    return _response;
                }



            }
        }

        [HttpGet("GetRolePrivilegeById{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetRolePrivilegeById(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Please Provide Id");

                var data = await _collegeRepository.GetbyId(x => x.Id == id);

                if (data == null)
                {
                    return NotFound("No data Available");
                }

                _response.data = _mapper.Map<RolePrivilegeDTO>(data);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Status = true;
                return _response;
            }
            catch (Exception ex)
            {
                {
                    _response.Errors.Add(ex.Message);
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.Status = false;
                    return _response;
                }



            }
        }
        [HttpGet("GetRolePrivilegeByName/{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetRolePrivilegeByName(string name)
        {
            try
            {
                if (name == "")
                    return BadRequest("Please Provide Id");

                var data = await _collegeRepository.GetbyId(x => x.RolePrivilegeName == name);

                if (data == null)
                {
                    return NotFound("No data Available");
                }

                _response.data = _mapper.Map<RolePrivilegeDTO>(data);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Status = true;
                return _response;
            }
            catch (Exception ex)
            {
                {
                    _response.Errors.Add(ex.Message);
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.Status = false;
                    return _response;
                }



            }
        }

        [HttpPut("UpdateRolePrivilege")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> UpdateRolePrivilege(RolePrivilegeDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Please Provide Id");
                var roleexist = await _collegeRepository.GetbyId(x => x.Id == dto.Id);
                
                
                if (roleexist == null)
                    return NotFound("Data is not available");

                var roleEntity = _mapper.Map<RolePrivilege>(dto);
                var data = await _collegeRepository.UpdateData(roleEntity);



                _response.data = _mapper.Map<RolePrivilegeDTO>(data);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Status = true;
                return _response;
            }
            catch (Exception ex)
            {
                {
                    _response.Errors.Add(ex.Message);
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.Status = false;
                    return _response;
                }



            }
        }

        [HttpDelete("RemoveRolePrivilegeById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> RemoveRolePrivilegeById(int id)
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
                    return NotFound($"The Role with name {id} not found");
                }

                _response.data = await _collegeRepository.DeleteData(stu);
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.Status = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(ex.Message);
                return _response;
            }
        }

        [HttpGet("GetRolePrivilegeByRoleId{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetRolePrivilegeByRoleId(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Please Provide Id");

                var data = await _collegeRepository.GetbyFilterId(x => x.RoleId == id);

                if (data == null)
                {
                    return NotFound("No data Available");
                }

                _response.data = _mapper.Map<List<RolePrivilegeDTO>>(data);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Status = true;
                return _response;
            }
            catch (Exception ex)
            {
                {
                    _response.Errors.Add(ex.Message);
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.Status = false;
                    return _response;
                }



            }
        }
    }
}
