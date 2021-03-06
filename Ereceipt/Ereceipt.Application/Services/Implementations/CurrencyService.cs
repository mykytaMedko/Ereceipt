using AutoMapper;
using Ereceipt.Application.Extensions;
using Ereceipt.Application.Results.Currencies;
using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.Currency;
using Ereceipt.Domain.Interfaces;
using Ereceipt.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Ereceipt.Application.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;
        public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        public async Task<CurrencyResult> CreateCurrencyAsync(CurrencyCreateModel model)
        {
            if (await _currencyRepository.CountAsync(d => d.Code == model.Code || d.ISOFormat == model.ISOFormat) > 0)
                return new CurrencyResult("This currency is already exist");
            var currencyForCreate = _mapper.Map<Currency>(model);
            currencyForCreate.SetInitData(model);
            return new CurrencyResult(_mapper.Map<CurrencyRootViewModel>(await _currencyRepository.CreateAsync(currencyForCreate)));
        }

        public async Task<CurrencyResult> EditCurrencyAsync(CurrencyEditModel model)
        {
            var currencyForEdit = await _currencyRepository.FindAsTrackingAsync(d => d.Id == model.Id);
            if (currencyForEdit is null)
                return new CurrencyResult("Currency for edit not found");
            currencyForEdit.Symbol = model.Symbol;
            currencyForEdit.Code = model.Code;
            currencyForEdit.ISOFormat = model.ISOFormat;
            currencyForEdit.Name = model.Name;
            currencyForEdit.Countries = model.Countries;
            currencyForEdit.SetUpdateData(model);
            var currencyEdited = _mapper.Map<CurrencyRootViewModel>(await _currencyRepository.UpdateAsync(currencyForEdit));
            return new CurrencyResult(currencyEdited);
        }

        public async Task<ListCurrenciesResult> GetAllCurrenciesAsync()
        {
            var currencies = _mapper.Map<List<CurrencyViewModel>>(await _currencyRepository.GetAllCurrenciesAsync());
            return new ListCurrenciesResult(currencies);
        }

        public async Task<ListCurrenciesResult> GetAllCurrenciesRootAsync()
        {
            var currencies = _mapper.Map<List<CurrencyRootViewModel>>(await _currencyRepository.GetAllCurrenciesAsync());
            return new ListCurrenciesResult(currencies);
        }

        public async Task<ListCurrenciesResult> GetCurrenciesByCodeAsync(string code)
        {
            var currencies = _mapper.Map<List<CurrencyViewModel>>(await _currencyRepository.GetByCodeAsync(code));
            return new ListCurrenciesResult(currencies);
        }

        public async Task<CurrencyResult> GetCurrencyByIdAsync(int id)
        {
            var currency = _mapper.Map<CurrencyRootViewModel>(await _currencyRepository.FindAsync(d => d.Id == id));
            return new CurrencyResult(currency);
        }

        public async Task<CurrencyResult> RemoveCurrencyAsync(int id)
        {
            var currencyForDelete = await _currencyRepository.FindAsTrackingAsync(d => d.Id == id);
            if (currencyForDelete is null)
                return new CurrencyResult("Currency for delete not found");
            var currencyDeleted = _mapper.Map<CurrencyRootViewModel>(await _currencyRepository.RemoveAsync(currencyForDelete));
            return new CurrencyResult(currencyDeleted);
        }
    }
}