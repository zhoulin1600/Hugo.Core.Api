using AutoMapper;
using System;

namespace Hugo.Core.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper关系映射
    /// </summary>
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 自定义DTO映射配置
        /// </summary>
        public CustomProfile()
        {
            //CreateMap<DataModel.Sys_User, DataView.View_Sys_User>().ReverseMap();
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
