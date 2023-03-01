using AutoMapper;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Implementations.Services;
using Daisy.Shared.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Infrastructure.Implementations.Interfaces
{
    public class ServiceManager : IServiceManager
    {
        public ICarouselService CarouselService { get; private set; }
        public IAppUserService AppUserService { get; private set; }
        public IEventService EventService { get; private set; }
        public IAppointmentService AppointmentService { get; private set; }
        public IAuthService AuthService { get; private set; }
        public IRoleService RoleService { get; private set; }
        public IEmailService EmailService { get; private set; }

        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IClaimsService claimsService;
        private readonly IJwtTokenService jwtTokenService;
        private readonly IConfiguration config;
        private readonly ILogger<AppUserService> logger;
        private readonly EmailConfiguration emailConfig;
        

        public ServiceManager(IUnitOfWork UnitOfWork, IMapper Mapper, UserManager<AppUser> UserManager, SignInManager<AppUser> SignInManager, IConfiguration Config, IClaimsService ClaimsService, IJwtTokenService JwtTokenService, EmailConfiguration EmailConfig)
        {
            unitOfWork = UnitOfWork;
            mapper = Mapper;
            userManager = UserManager;
            signInManager = SignInManager;
            config = Config;
            claimsService = ClaimsService;
            jwtTokenService = JwtTokenService;
            emailConfig = EmailConfig;

            //AppUserService = new AppUserService(unitOfWork, mapper, userManager, logger);
            AppUserService = new AppUserService(unitOfWork, mapper, userManager);
            CarouselService = new CarouselService(unitOfWork, mapper);
            EventService = new EventService(unitOfWork, mapper);
            AppointmentService = new AppointmentService(unitOfWork, mapper, userManager);
            AuthService = new AuthService(unitOfWork, mapper, signInManager, userManager, config, claimsService, jwtTokenService);
            RoleService = new RoleService();
            EmailService = new EmailService(emailConfig);
        }

    }
}
