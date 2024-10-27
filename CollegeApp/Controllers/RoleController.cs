using AutoMapper;
using CollegeApp.DTO;
using CollegeApp.Model;
using CollegeApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICollegeRepository<Roles> _collegeRepository;
        private ApiResponse _response;
        public RoleController(IMapper mapper, ICollegeRepository<Roles> collegeRepository)
        {
            _mapper = mapper;
            _response = new ApiResponse();
            _collegeRepository = collegeRepository;
        }


        [HttpPost("CreateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateRole(RoleDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Please provide Role");
                }

                var data = _mapper.Map<Roles>(dto);
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

        [HttpGet("GetAllRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetAllRole()
        {
            try
            {
                
                var data  = await _collegeRepository.GetAll();
                
                if (data == null)
                {
                  return  BadRequest("No data Available");
                }
                
                _response.data = _mapper.Map<List<RoleDTO>>(data);
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

        [HttpGet("GetRoleById{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetRoleById(int id)
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

                _response.data = _mapper.Map<RoleDTO>(data);
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
        [HttpGet("GetRoleByName/{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetRoleByName(string name)
        {
            try
            {
                if (name == "")
                    return BadRequest("Please Provide Id");

                var data = await _collegeRepository.GetbyId(x => x.RoleName == name);

                if (data == null)
                {
                    return NotFound("No data Available");
                }

                _response.data = _mapper.Map<RoleDTO>(data); 
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

        [HttpPut("UpdateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> UpdateRole(RoleDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Please Provide Id");
              
                var roleexist = await _collegeRepository.GetbyId(x => x.Id == dto.Id);
                if (roleexist == null) 
                    return NotFound("Data is not available");

                var roleEntity = _mapper.Map<Roles>(dto);
                var data = await _collegeRepository.UpdateData(roleEntity);

               

                _response.data = _mapper.Map<RoleDTO>(data);
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

        [HttpDelete("RemoveRoleById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> RemoveRoleById(int id)
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

    }
}
