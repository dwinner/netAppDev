/**
 * Автоматическое отображение одного объекта в другой
 */

using System;

namespace AutoMapper.Simple
{
    internal static class Program
    {
        private static void Main()
        {
            // The configuration of mapping one object to another
            Mapper.CreateMap<UserDto, User>()
                .ForMember(user => user.FullName,
                    expression
                    		=> expression.MapFrom(
                    			 	dto => string.Format("{0} {1}", dto.FirstName, dto.LastName)))
                .ForMember(user => user.Email, expression => expression.MapFrom(dto => dto.Email));

            // Convert the object via auto mapping
            var mappedUser = Mapper.Map<User>(new UserDto
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "ivan@ivanov.ru"
            });

            Console.WriteLine(mappedUser);
        }
    }

    internal class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    internal class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return string.Format("FullName: {0}, Email: {1}", FullName, Email);
        }
    }
}