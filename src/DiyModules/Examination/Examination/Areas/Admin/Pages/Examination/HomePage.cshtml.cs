using Examination.Models.DatabaseModel.Dto;
using Examination.Services;
using Microsoft.AspNetCore.Mvc;
using Senparc.Scf.Core.Enums;
using Senparc.Scf.Service;
using System;
using System.Threading.Tasks;

namespace Senparc.Xscf.ExtensionAreaTemplate.Areas.MyApp.Pages
{
    public class MyHomePage : Senparc.Scf.AreaBase.Admin.AdminXscfModulePageModelBase
    {
        public ColorDto ColorDto { get; set; }

        private readonly ColorService _colorService;
        private readonly IServiceProvider _serviceProvider;
        public MyHomePage(IServiceProvider serviceProvider, ColorService colorService, Lazy<XscfModuleService> xscfModuleService)
            : base(xscfModuleService)
        {
            _colorService = colorService;
            _serviceProvider = serviceProvider;
        }

        public Task OnGetAsync()
        {
            var color = _colorService.GetObject(z => true, z => z.Id, OrderingType.Descending);
            ColorDto = _colorService.Mapper.Map<ColorDto>(color);
            return Task.CompletedTask;
        }

        public IActionResult OnGetDetail()
        {
            var color = _colorService.GetObject(z => true, z => z.Id, OrderingType.Descending);
            var colorDto = _colorService.Mapper.Map<ColorDto>(color);
            //return Task.CompletedTask;
            return Ok(new { colorDto, XscfModuleDto });
        }

        public async Task<IActionResult> OnGetBrightenAsync()
        {
            var colorDto = await _colorService.Brighten().ConfigureAwait(false);
            return Ok(colorDto);
        }

        public async Task<IActionResult> OnGetDarkenAsync()
        {
            var colorDto = await _colorService.Darken().ConfigureAwait(false);
            return Ok(colorDto);
        }
        public async Task<IActionResult> OnGetRandomAsync()
        {
            var colorDto = await _colorService.Random().ConfigureAwait(false);
            return Ok(colorDto);
        }
    }
}
