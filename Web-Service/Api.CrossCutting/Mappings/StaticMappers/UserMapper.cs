using Api.Domain.Commands.UserCommands;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;

namespace Api.CrossCutting.Mappings.StaticMappers
{
    public static class UserMapper
    {
        public static UserView ToView(this User user)
        {
            if (user == null) return null!;

            return new UserView
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Gender = user.Gender,
                AddressList = user.AddressList ?? new List<Address>(),
                ContactList = user.ContactList ?? new List<Contact>()
            };
        }

        public static User ToEntity(this UserView view)
        {
            if (view == null) return null!;

            return new User
            {
                Id = view.Id,
                Name = view.Name!,
                Email = view.Email!,
                Gender = view.Gender,
                AddressList = view.AddressList,
                ContactList = view.ContactList
            };
        }

        public static List<UserView> ToViewList(this IEnumerable<User> users)
        {
            return users?.Select(u => u.ToView()).ToList() ?? new List<UserView>();
        }

        public static CreateNewUserCommand ToCreateCommand(this CreateUserRequest request)
        {
            if (request == null) return null!;

            return new CreateNewUserCommand
            {
                Name = request.Name,
                Email = request.Email,
                Gender = request.Gender
            };
        }

        public static UpdateUserCommand ToUpdateCommand(this UpdateUserRequest request)
        {
            if (request == null) return null!;

            return new UpdateUserCommand
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Gender = request.Gender
            };
        }

        public static AddAddressToUserCommand ToAddAddressCommand(this AddAddressRequest request)
        {
            if (request == null) return null!;

            return new AddAddressToUserCommand
            {
                UserId = request.UserId,
                Street = request.Street,
                Number = request.Number,
                District = request.District,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode
            };
        }

        public static UpdateUserAddressCommand ToUpdateAddressCommand(this UpdateAddressRequest request)
        {
            if (request == null) return null!;

            return new UpdateUserAddressCommand
            {
                UserId = request.UserId,
                AddressId = request.AddressId,
                Street = request.Street,
                Number = request.Number,
                District = request.District,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode
            };
        }
    }
} 