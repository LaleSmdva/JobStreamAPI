using AutoMapper.Internal;
using JobStream.Business.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces;

public interface IMailService
{
	Task SendEmailAsync(MailRequestDTO mailRequestDTO);
}
