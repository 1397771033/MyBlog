using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Service
{
  public class WriterInfoService:BaseService<WriterInfo>,IWriterInfoService
  {
    private readonly IWriterInfoRepository _iWriterInfoRepository;
    public WriterInfoService(IWriterInfoRepository iWriterInfoRepository)
    {
      base._iBaseRepository = iWriterInfoRepository;
      _iWriterInfoRepository = iWriterInfoRepository;
    }
  }
}
