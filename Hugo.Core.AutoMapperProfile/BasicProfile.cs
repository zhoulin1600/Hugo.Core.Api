using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper关系映射
    /// </summary>
    public class BasicProfile : Profile
    {
        /// <summary>
        /// 基础DTO映射配置
        /// </summary>
        public BasicProfile()
        {
            #region Sys

            CreateMap<DataModel.Sys_User, DataView.View_Sys_User>().ReverseMap();
            CreateMap<DataModel.Sys_Role, DataView.View_Sys_Role>().ReverseMap();
            CreateMap<DataModel.Sys_UserRole, DataView.View_Sys_UserRole>().ReverseMap();
            CreateMap<DataModel.Sys_Menu, DataView.View_Sys_Menu>().ReverseMap();
            CreateMap<DataModel.Sys_Action, DataView.View_Sys_Action>().ReverseMap();
            CreateMap<DataModel.Sys_Permission, DataView.View_Sys_Permission>().ReverseMap();

            CreateMap<DataModel.Sys_Application, DataView.View_Sys_Application>().ReverseMap();

            #endregion

        }

    }
}
/*****************************************帮助文档***********************************************
 * ReverseMap() 双向映射
 * 
    // 属性名称不一样
    CreateMap<Student, StudentViewModel>()
        .ForMember(d => d.CountyName, o => o.MapFrom(s => s.County))
        .ForMember(d => d.ProvinceName, o => o.MapFrom(s => s.Province));

    // 有子类的复杂类型
    CreateMap<Student, StudentViewModel>()
        .ForMember(d => d.County, o => o.MapFrom(s => s.Address.County))
        .ForMember(d => d.Province, o => o.MapFrom(s => s.Address.Province))
        .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City))
        .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street));
 * 
 * *********************************************************************************************/