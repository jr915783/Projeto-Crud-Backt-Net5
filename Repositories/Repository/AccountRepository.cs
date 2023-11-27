using AutoMapper;
using Domain.Dtos;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class AccountRepository : IAccount
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;
        public AccountRepository(UserManager<User> userManager, SignInManager<User> singInManager, IMapper mapper, IUserPersist userPersist)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _mapper = mapper;
            _userPersist = userPersist;
        }
        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var  user  = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName == userUpdateDto.UserName.ToLower());

                return await _singInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                ((Microsoft.AspNetCore.Identity.IdentityUser<int>)user).UserName = userDto.UserName;
                var result =  await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    var userReturn = _mapper.Map<UserUpdateDto>(user);
                    return userReturn;
                }
                else
                {
                    var errors = result.Errors;
                }

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar criar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccountAsync(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userPersist.GetUserByUserNameAsync(userUpdateDto.UserName);
                if (user == null) return null;

                _mapper.Map(userUpdateDto, user);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result =  await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                _userPersist.Update(1,user);

               var userReturn = await  _userPersist.GetUserByUserNameAsync(user.UserName);

                return  _mapper.Map<UserUpdateDto>(userReturn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
             try
            {
                var user = await _userPersist.GetUserByUserNameAsync(userName);
                if (user == null) return null;

                var userReturn = _mapper.Map<UserUpdateDto>(user);
                return userReturn;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar pegar usuário por username. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string username)
        {
             try
            {
                return await _userManager.Users.AnyAsync( userName => userName.UserName == username);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar verificar se usuário existe. Erro: {ex.Message}");
            }
        }
    }
}
