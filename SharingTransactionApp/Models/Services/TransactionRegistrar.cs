using MongoDB.Driver;
using NHibernate;
using SharingTransactionApp.Enums;
using SharingTransactionApp.Models.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models.Services
{
    public class TransactionRegistrar: ITransactionRegistrar
    {
        private readonly ISession _session;

        public TransactionRegistrar(ISession session)
        {
            _session = session;
        }
        public bool Register(TransactionInput transaction,string author)
        {
            var decoded = Translate(transaction);
            decoded.transaction.Creator = GetUser(author);
            decoded.transaction.Shareholders = GetShareholders(transaction.Shareholders, author);
            using(_session.BeginTransaction())
            {
                _session.Save(decoded.image);
                _session.Save(decoded.transaction);
                _session.GetCurrentTransaction()?.Commit();
            }
            return true;
        }
        private (Transaction transaction,ImageJson image) Translate(TransactionInput input)
        {
            var guidImage = Guid.NewGuid();
            var guidTransaction = Guid.NewGuid();
            var date = DateTime.Now;
            FormatEnum format = GetFormat(input.Format);
            var imgJson = new ImageJson { Id = guidImage, Data = input.File,Format=format };
            var transacion = new Transaction
            {
                Title=input.Title,
                Description=input.Description,
                Cash = input.Cost,
                Date = date,
                Id = guidTransaction,
                File = guidImage
            };
            return (transacion,imgJson);
        }

        private List<UserConfirmation> GetShareholders(List<string> shareholders, string creator)
        {
            return shareholders.Select(s => s == creator ? (new UserConfirmation {Person= GetUser(s),Confirmation= true }) 
            : (new UserConfirmation { Person = GetUser(s), Confirmation = false })).ToList();
        }

        private AppUser GetUser(string name)
        {
            return _session.Query<AppUser>().Where(user=>user.Name==name).First();
        }
        private FormatEnum GetFormat(string format)
        {
            return format switch
            {
                string text when text.Contains("pdf") => FormatEnum.pdf,
                string text when text.Contains("png") => FormatEnum.png,
                string text when text.Contains("jpg") => FormatEnum.jpg,
                _ => throw new NotSupportedException(),
            };
        }
    }
}
