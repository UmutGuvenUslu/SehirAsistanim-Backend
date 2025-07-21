using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;

namespace SehirAsistanim.Infrastructure.Services
{

    public class SikayetService:ISikayetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtService _jwtService;
        private readonly IDuyguAnaliz _duyguAnalizService;

        public SikayetService(IUnitOfWork unitOfWork,IDuyguAnaliz duyguAnaliz, JwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _duyguAnalizService = duyguAnaliz;
        }

        public async Task<List<Sikayet>> GetAll()
        {
           return  _unitOfWork.Repository<Sikayet>().GetAll().Result.ToList();
        }
        public Task<Sikayet> GetById(int sikayetId)
        {
            throw new NotImplementedException();
        }
        public async Task<Sikayet> AddSikayet(Sikayet sikayet)
        {
            sikayet.DuyguPuani = _duyguAnalizService.HesaplaDuyguPuani(sikayet.Aciklama);
            await _unitOfWork.Repository<Sikayet>().Add(sikayet);
            await _unitOfWork.Commit();
            return sikayet;
        }
    }
}
