using System;
using System.Linq;
using AutoMapper;
using ASPNETCoreWebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using ASPNETCoreWebAPI.Repositories;
using System.Collections.Generic;
using ASPNETCoreWebAPI.Entities;
using ASPNETCoreWebAPI.Models;
using ASPNETCoreWebAPI.Helpers;
using Newtonsoft.Json;

namespace ASPNETCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUrlHelper _urlHelper;

        public CustomersController(IUrlHelper urlHelper, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(GetAll))]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters)
        {
            List<Customer> allCustomers = _customerRepository.GetAll(queryParameters).ToList();

            var allItemCount = _customerRepository.Count();

            var paginationMetadata = new
            {
                totalCount = allItemCount,
                pageSize = queryParameters.PageCount,
                currentPage = queryParameters.Page,
                totalPages = queryParameters.GetTotalPages(allItemCount)
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

            var links = CreateLinksForCollection(queryParameters, allItemCount);

            var toReturn = allCustomers.Select(x => ExpandSingleItem(x));

            return Ok(new
            {
                value = toReturn,
                links = links
            });
        }

        [HttpGet]
        [Route("{id:int}", Name = nameof(GetSingle))]
        public IActionResult GetSingle(int id)
        {
            Customer customer = _customerRepository.GetSingle(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(ExpandSingleItem(customer));
        }

        [HttpPost(Name = nameof(Add))]
        public IActionResult Add([FromBody] CustomerCreateDto customerCreateDto)
        {
            if (customerCreateDto == null)
            {
                return BadRequest();
            }

            Customer toAdd = Mapper.Map<Customer>(customerCreateDto);

            toAdd.Created = DateTime.Now;
            _customerRepository.Add(toAdd);

            if (!_customerRepository.Save())
            {
                throw new Exception("Creating an item failed on save.");
            }

            Customer newItem = _customerRepository.GetSingle(toAdd.Id);

            return CreatedAtRoute(nameof(GetSingle), new { id = newItem.Id },
                Mapper.Map<CustomerDto>(newItem));
        }

        [HttpDelete]
        [Route("{id:int}", Name = nameof(Delete))]
        public IActionResult Delete(int id)
        {
            Customer customer = _customerRepository.GetSingle(id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.Delete(id);

            if (!_customerRepository.Save())
            {
                throw new Exception("Deleting an item failed on save.");
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}", Name = nameof(Update))]
        public IActionResult Update(int id, [FromBody]CustomerUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }

            var existingCustomer = _customerRepository.GetSingle(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            Mapper.Map(updateDto, existingCustomer);

            _customerRepository.Update(id, existingCustomer);

            if (!_customerRepository.Save())
            {
                throw new Exception("Updating an item failed on save.");
            }

            return Ok(ExpandSingleItem(existingCustomer));
        }

        private List<LinkDto> CreateLinksForCollection(QueryParameters queryParameters, int totalCount)
        {
            var links = new List<LinkDto>();

            links.Add(
             new LinkDto(_urlHelper.Link(nameof(Add), null), "create", "POST"));

            // self 
            links.Add(
             new LinkDto(_urlHelper.Link(nameof(GetAll), new
             {
                 pagecount = queryParameters.PageCount,
                 page = queryParameters.Page,
                 orderby = queryParameters.OrderBy
             }), "self", "GET"));

            links.Add(new LinkDto(_urlHelper.Link(nameof(GetAll), new
            {
                pagecount = queryParameters.PageCount,
                page = 1,
                orderby = queryParameters.OrderBy
            }), "first", "GET"));

            links.Add(new LinkDto(_urlHelper.Link(nameof(GetAll), new
            {
                pagecount = queryParameters.PageCount,
                page = queryParameters.GetTotalPages(totalCount),
                orderby = queryParameters.OrderBy
            }), "last", "GET"));

            if (queryParameters.HasNext(totalCount))
            {
                links.Add(new LinkDto(_urlHelper.Link(nameof(GetAll), new
                {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page + 1,
                    orderby = queryParameters.OrderBy
                }), "next", "GET"));
            }

            if (queryParameters.HasPrevious())
            {
                links.Add(new LinkDto(_urlHelper.Link(nameof(GetAll), new
                {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page - 1,
                    orderby = queryParameters.OrderBy
                }), "previous", "GET"));
            }

            return links;
        }

        private dynamic ExpandSingleItem(Customer customer)
        {
            var links = GetLinks(customer.Id);
            CustomerDto item = Mapper.Map<CustomerDto>(customer);

            var resourceToReturn = item.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(int id)
        {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetSingle), new { id = id }),
              "self",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(Delete), new { id = id }),
              "delete",
              "DELETE"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(Add), null),
              "create",
              "POST"));

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(Update), new { id = id }),
               "update",
               "PUT"));

            return links;
        }
    }
}
