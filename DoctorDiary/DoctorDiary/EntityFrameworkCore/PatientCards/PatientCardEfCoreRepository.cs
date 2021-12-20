using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Shared.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoctorDiary.EntityFrameworkCore.PatientCards
{
    public class PatientCardEfCoreRepository : RepositoryBase<PatientCard, Guid>, IPatientCardRepository
    {
        public async Task<List<PatientCard>> PatientCardsByVisits(DateTime date, bool asNoTracking = false)
        {
            return null;
            //EntityDbSet.FromSqlInterpolated("SELECT * FROM Visits").ToListAsync();
        }

        public async Task<List<PatientCard>> GetLastCreatedPatientCards(
            int takeCount, 
            int skipCount, 
            bool asNoTracking = false, 
            string firstName = null,
            string lastName = null, 
            string patronymic = null, 
            string city = null, 
            string street = null, 
            string apartment = null,
            string house = null, 
            DateTime? birthday = null, 
            string snils = null, 
            string description = null,
            string phoneNumber = null, 
            string gender = null, 
            string insurancePolicy = null, 
            string placeOfWork = null,
            int? precinct = null)
        {
            var query = ApplyFilter(
                query: EntityDbSet,
                firstName: firstName,
                lastName: lastName,
                patronymic: patronymic,
                city: city,
                street: street,
                apartment: apartment,
                house: house,
                birthday: birthday,
                snils: snils,
                description: description,
                phoneNumber: phoneNumber,
                gender: gender,
                insurancePolicy: insurancePolicy,
                placeOfWork: placeOfWork,
                precinct: precinct);

            if (asNoTracking)
            {
                query.AsNoTracking();
            }
            
            return await query.OrderByDescending(x => x.CreationTime)
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();
        }

        public async Task<List<PatientCard>> GetLastCreatedPatientCards(
            int takeCount, 
            int skipCount,
            bool asNoTracking = false)
        {
            var query = EntityDbSet
                .OrderByDescending(x => x.CreationTime)
                .Skip(skipCount)
                .Take(takeCount);

            if (asNoTracking)
            {
                query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<List<PatientCard>> GetListByFilterAsync(int takeCount, int skipCount, bool asNoTracking = false)
        {
            var query = EntityDbSet.Where(x => x.Precinct.ToString().Contains("34") ||
                                               x.Snils.ToString().Contains("88883333221"))
                .OrderByDescending(x => x.CreationTime);

            if (asNoTracking)
            {
                query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        private IQueryable<PatientCard> ApplyFilter(
            IQueryable<PatientCard> query, 
            string firstName = null,
            string lastName = null, 
            string patronymic = null, 
            string city = null, 
            string street = null, 
            string apartment = null,
            string house = null, 
            DateTime? birthday = null, 
            string snils = null, 
            string description = null,
            string phoneNumber = null, 
            string gender = null, 
            string insurancePolicy = null, 
            string placeOfWork = null,
            int? precinct = null)
        {
            return query
                .WhereIf(!string.IsNullOrEmpty(firstName), p => p.FirstName == firstName)
                .WhereIf(!string.IsNullOrEmpty(lastName), p => p.LastName == lastName)
                .WhereIf(!string.IsNullOrEmpty(patronymic), p => p.Patronymic == patronymic)
                .WhereIf(!string.IsNullOrEmpty(city), p => p.Address.City == city)
                .WhereIf(!string.IsNullOrEmpty(street), p => p.Address.Street == street)
                .WhereIf(!string.IsNullOrEmpty(apartment), p => p.Address.Apartment == apartment)
                .WhereIf(!string.IsNullOrEmpty(house), p => p.Address.House == house)
                .WhereIf(birthday.HasValue, p => p.Birthday == birthday)
                .WhereIf(!string.IsNullOrEmpty(snils), p => p.Snils.Value == snils)
                .WhereIf(!string.IsNullOrEmpty(description), p => p.Description == description)
                .WhereIf(!string.IsNullOrEmpty(phoneNumber), p => p.PhoneNumber.Value == phoneNumber)
                .WhereIf(!string.IsNullOrEmpty(gender), p => p.Gender == gender)
                .WhereIf(!string.IsNullOrEmpty(insurancePolicy), p => p.InsurancePolicy.Value == insurancePolicy)
                .WhereIf(!string.IsNullOrEmpty(placeOfWork), p => p.PlaceOfWork == placeOfWork)
                .WhereIf(precinct.HasValue, p => p.Precinct == precinct);
        }
    }
}