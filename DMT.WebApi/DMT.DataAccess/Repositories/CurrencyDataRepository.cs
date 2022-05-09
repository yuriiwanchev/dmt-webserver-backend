// using System.Collections.Generic;
// using System.Linq;
// using AutoMapper;
// using DataAccess.Models;
// using Domain.Helpers;
// using Domain.Models;
// using Domain.Repositories;
// using Microsoft.EntityFrameworkCore;
//
// namespace DataAccess.Repositories
// {
//     internal class CurrencyDataRepository : ICurrencyDataRepository
//     {
//         private readonly CurrencyDbContext context;
//         private readonly IMapper mapper;
//         
//         public CurrencyDataRepository(CurrencyDbContext DbContext, IMapper mapper)
//         {
//             context = DbContext;
//             this.mapper = mapper;
//         }
//         
//         public void New(CurrencyData currencyData)
//         {
//             var itemDb = mapper.Map<CurrencyDataDb>(currencyData);
//             var result = context.CurrencyDataDbs.Add(itemDb);
//             context.SaveChanges();
//         }
//
//         public CurrencyData? Get(string parentCode)
//         {
//             var itemDb = context.CurrencyDataDbs.FirstOrDefault(x => x.ParentCode == parentCode);
//             return mapper.Map<CurrencyData?>(itemDb);
//         }
//
//         public IEnumerable<CurrencyData> GetAll()
//         {
//             var itemsDb = context.CurrencyDataDbs.ToList();
//             return mapper.Map<IReadOnlyCollection<CurrencyData>>(itemsDb);
//         }
//
//         public void Edit(CurrencyData currencyData)
//         {
//             if (context.CurrencyDataDbs.Find(currencyData.ParentCode) is CurrencyDataDb currencyDataInDb)
//             {
//                 currencyDataInDb.Name = currencyData.Name;
//                 currencyDataInDb.Nominal = currencyData.Nominal;
//                 currencyDataInDb.EngName = currencyData.EngName;
//                 currencyDataInDb.ParentCode = currencyData.ParentCode;
//                 currencyDataInDb.IsoNumCode = currencyData.IsoNumCode;
//                 currencyDataInDb.IsoCharCode = currencyData.IsoCharCode;
//                 context.Entry(currencyDataInDb).State = EntityState.Modified;
//                 context.SaveChanges();
//             }
//             else
//             {
//                 throw new InvalidUserInputException("There is no that currencyData");
//             }
//         }
//
//         public void Delete(string parentCode)
//         {
//             var itemToDelete = context.CurrencyDataDbs.Find(parentCode);
//             context.Entry(itemToDelete).State = EntityState.Deleted;
//             context.SaveChanges();
//         }
//     }
// }