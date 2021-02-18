using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Service
{
  public class TypeInfoService:BaseService<TypeInfo>,ITypeInfoService
  {
    private readonly ITypeInfoRepository _iTypeInfoRepository;
    public TypeInfoService(ITypeInfoRepository iTypeInfoRepository)
    {
      base._iBaseRepository = iTypeInfoRepository;
      _iTypeInfoRepository = iTypeInfoRepository;
    }
  }
}
