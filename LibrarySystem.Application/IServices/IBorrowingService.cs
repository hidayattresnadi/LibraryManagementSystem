using LibrarySystem.Application.DTO.UserDTO;
using LibrarySystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.IServices
{
    public interface IBorrowingService
    {
        Task<UserCheckout> CheckOutBooks(string ISBN, string LibraryCardNumber);
    }
}
