using Medixa_AI.Application.DTOs;
using Medixa_AI.Application.Interfaces;
using Medixa_AI.Domain.Entities;

namespace Medixa_AI.Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepository<OrderDetail> _repository;

        public OrderDetailService(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderDetailDto>> GetAllAsync()
        {
            var orderDetails = await _repository.GetAllAsync();
            return orderDetails.Select(MapToDto);
        }

        public async Task<OrderDetailDto?> GetByIdAsync(Guid id)
        {
            var orderDetail = await _repository.GetByIdAsync(id);
            return orderDetail == null ? null : MapToDto(orderDetail);
        }

        public async Task<OrderDetailDto?> CreateAsync(OrderDetailDto dto)
        {
            var orderDetail = MapToEntity(dto);
            await _repository.AddAsync(orderDetail);
            await _repository.SaveChangesAsync();
            return MapToDto(orderDetail);
        }

        public async Task<bool> UpdateAsync(Guid id, OrderDetailDto dto)
        {
            var orderDetail = await _repository.GetByIdAsync(id);
            if (orderDetail == null)
                return false;

            orderDetail.OrderID = dto.OrderID;
            orderDetail.TestID = dto.TestID;
            orderDetail.Price = dto.Price;
            orderDetail.Status = dto.Status;
            orderDetail.IsAbnormal = dto.IsAbnormal;
            orderDetail.CompletedAt = dto.CompletedAt;

            _repository.Update(orderDetail);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var orderDetail = await _repository.GetByIdAsync(id);
            if (orderDetail == null)
                return false;

            _repository.Delete(orderDetail);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderDetailDto>> GetByOrderAsync(Guid orderId)
        {
            var orderDetails = await _repository.GetAllAsync();
            return orderDetails.Where(od => od.OrderID == orderId).Select(MapToDto);
        }

        public async Task<IEnumerable<OrderDetailDto>> GetByTestAsync(Guid testId)
        {
            var orderDetails = await _repository.GetAllAsync();
            return orderDetails.Where(od => od.TestID == testId).Select(MapToDto);
        }

        private static OrderDetailDto MapToDto(OrderDetail orderDetail)
        {
            return new OrderDetailDto
            {
                OrderDetailID = orderDetail.OrderDetailID,
                OrderID = orderDetail.OrderID,
                TestID = orderDetail.TestID,
                Price = orderDetail.Price,
                Status = orderDetail.Status,
                IsAbnormal = orderDetail.IsAbnormal,
                CompletedAt = orderDetail.CompletedAt
            };
        }

        private static OrderDetail MapToEntity(OrderDetailDto dto)
        {
            return new OrderDetail
            {
                OrderDetailID = dto.OrderDetailID,
                OrderID = dto.OrderID,
                TestID = dto.TestID,
                Price = dto.Price,
                Status = dto.Status,
                IsAbnormal = dto.IsAbnormal,
                CompletedAt = dto.CompletedAt
            };
        }
    }
}
