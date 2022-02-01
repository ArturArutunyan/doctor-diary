using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Shared.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoctorDiary.EntityFrameworkCore.PatientCards
{
    public class PatientCardEfCoreRepository : RepositoryBase<PatientCard, Guid>, IPatientCardRepository
    {
        public async Task<List<PatientCard>> GetLastCreatedPatientCards(int takeCount,
            int skipCount,
            bool asNoTracking = false,
            string textFilter = null,
            string firstName = null,
            string lastName = null,
            string patronymic = null,
            string city = null,
            string street = null,
            string apartment = null,
            string house = null,
            DateTime? birthday = null,
            int? yearOfBirth = null,
            string snils = null,
            string description = null,
            string phoneNumber = null,
            string gender = null,
            string insurancePolicy = null,
            string placeOfWork = null,
            int? precinct = null)
        {
            var sqlParameters = new List<SqlParameter>();
            var whereTerms = new List<string>();

            if (!string.IsNullOrEmpty(textFilter))
            {
                var searchTerms = textFilter.Split(" ").Select(x => x.ToUpper()).ToArray();
                
                foreach (var searchTerm in searchTerms)
                {
                    whereTerms.AddRange(new List<string>()
                    {
                        $"(CYR_UPPER(FirstName) LIKE ('%{searchTerm}%') COLLATE UTF8CI)",
                        $"(CYR_UPPER(LastName) LIKE ('%{searchTerm}%') COLLATE UTF8CI)",
                        $"(CYR_UPPER(Patronymic) LIKE ('%{searchTerm}%') COLLATE UTF8CI)"
                    });
                }
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                whereTerms.Add($"(CYR_UPPER(FirstName) LIKE ('%{firstName}%') COLLATE UTF8CI)");
            }
            
            if (!string.IsNullOrEmpty(lastName))
            {
                whereTerms.Add($"(CYR_UPPER(LastName) LIKE ('%{lastName}%') COLLATE UTF8CI)");
            }

            if (!string.IsNullOrEmpty(patronymic))
            {
                whereTerms.Add($"(CYR_UPPER(Patronymic) LIKE ('%{patronymic}%') COLLATE UTF8CI)");
            }
            
            if (!string.IsNullOrEmpty(city))
            {
                whereTerms.Add($"(CYR_UPPER(City) LIKE ('%{city}%') COLLATE UTF8CI)");
            }
            
            if (!string.IsNullOrEmpty(street))
            {
                whereTerms.Add($"(CYR_UPPER(Street) LIKE ('%{street}%') COLLATE UTF8CI)");
            }
            
            if (!string.IsNullOrEmpty(apartment))
            {
                whereTerms.Add($"(CYR_UPPER(Apartment) LIKE ('%{apartment}%') COLLATE UTF8CI)");
            }
            
            if (!string.IsNullOrEmpty(house))
            {
                whereTerms.Add($"(CYR_UPPER(House) LIKE ('%{house}%') COLLATE UTF8CI)");
            }
            
            if (birthday.HasValue)
            {
                whereTerms.Add($"(Birthday = @birthday)");
                sqlParameters.Add(new SqlParameter("@birthday", birthday));
            }
            
            if (!string.IsNullOrEmpty(snils))
            {
                whereTerms.Add($"(Snils LIKE ('%{snils}%'))");
            }

            if (!string.IsNullOrEmpty(description))
            {
                whereTerms.Add($"(Description LIKE ('%{description}%'))");
            }
            
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                whereTerms.Add($"(PhoneNumber LIKE ('%{phoneNumber}%'))");
            }
            
            if (!string.IsNullOrEmpty(gender))
            {
                whereTerms.Add($"(Gender == @gender)");
                sqlParameters.Add(new SqlParameter("@gender", gender));
            }
            
            var where = whereTerms.Any() ? $"WHERE {string.Join(" OR ", whereTerms)}" : string.Empty;
            var query = $@"
                    SELECT p.* FROM PatientCards AS p {where}
                    ORDER BY p.CreationTime DESC
                    LIMIT {takeCount} OFFSET {skipCount}";
            
            var patientCards = await EntityDbSet.FromSqlRaw(query, sqlParameters.ToArray()).AsNoTracking().ToListAsync();

            // TODO: move to sql query
            if (yearOfBirth.HasValue)
            {
                patientCards = patientCards.Where(p => p.Birthday.HasValue && p.Birthday.Value.Year == yearOfBirth).ToList();
            }

            return patientCards;
        }
        
        // public async Task<List<PatientCard>> GetLastCreatedPatientCards(int takeCount,
        //     int skipCount,
        //     bool asNoTracking = false,
        //     string textFilter = null,
        //     string firstName = null,
        //     string lastName = null,
        //     string patronymic = null,
        //     string city = null,
        //     string street = null,
        //     string apartment = null,
        //     string house = null,
        //     DateTime? birthday = null,
        //     int? yearOfBirth = null,
        //     string snils = null,
        //     string description = null,
        //     string phoneNumber = null,
        //     string gender = null,
        //     string insurancePolicy = null,
        //     string placeOfWork = null,
        //     int? precinct = null)
        // {
        //     var query = ApplyFilter(
        //         query: EntityDbSet,
        //         firstName: firstName,
        //         lastName: lastName,
        //         patronymic: patronymic,
        //         city: city,
        //         street: street,
        //         apartment: apartment,
        //         house: house,
        //         birthday: birthday,
        //         yearOfBirth: yearOfBirth,
        //         snils: snils,
        //         description: description,
        //         phoneNumber: phoneNumber,
        //         gender: gender,
        //         insurancePolicy: insurancePolicy,
        //         placeOfWork: placeOfWork,
        //         precinct: precinct);
        //
        //     if (asNoTracking)
        //     {
        //         query.AsNoTracking();
        //     }
        //
        //     query = query.OrderByDescending(x => x.CreationTime)
        //         .Skip(skipCount)
        //         .Take(takeCount);
        //
        //     var s = query.ToQueryString();
        //     
        //     var patientCards = await query
        //         .ToListAsync();
        //     
        //     // TODO: move to sql query
        //     if (yearOfBirth.HasValue)
        //     {
        //         patientCards = patientCards.Where(p => p.Birthday.HasValue && p.Birthday.Value.Year == yearOfBirth).ToList();
        //     }
        //
        //     return patientCards;
        // }
        //
        // private IQueryable<PatientCard> ApplyFilter(IQueryable<PatientCard> query,
        //     string firstName = null,
        //     string lastName = null,
        //     string patronymic = null,
        //     string city = null,
        //     string street = null,
        //     string apartment = null,
        //     string house = null,
        //     DateTime? birthday = null,
        //     int? yearOfBirth = null,
        //     string snils = null,
        //     string description = null,
        //     string phoneNumber = null,
        //     string gender = null,
        //     string insurancePolicy = null,
        //     string placeOfWork = null,
        //     int? precinct = null)
        // {
        //     return query
        //         .WhereIf(!string.IsNullOrEmpty(firstName), p => p.FirstName.ToLower().Contains(firstName.ToLower()))
        //         .WhereIf(!string.IsNullOrEmpty(lastName), p => p.LastName == lastName)
        //         .WhereIf(!string.IsNullOrEmpty(patronymic), p => p.Patronymic == patronymic)
        //         .WhereIf(!string.IsNullOrEmpty(city), p => p.Address.City == city)
        //         .WhereIf(!string.IsNullOrEmpty(street), p => p.Address.Street == street)
        //         .WhereIf(!string.IsNullOrEmpty(apartment), p => p.Address.Apartment == apartment)
        //         .WhereIf(!string.IsNullOrEmpty(house), p => p.Address.House == house)
        //         .WhereIf(birthday.HasValue, p => p.Birthday == birthday)
        //         // .WhereIf(yearOfBirth.HasValue, p => p.Birthday.Year == yearOfBirth.Value.Year)
        //         .WhereIf(!string.IsNullOrEmpty(snils), p => p.Snils.Value == snils)
        //         .WhereIf(!string.IsNullOrEmpty(description), p => p.Description == description)
        //         .WhereIf(!string.IsNullOrEmpty(phoneNumber), p => p.PhoneNumber.Value == phoneNumber)
        //         .WhereIf(!string.IsNullOrEmpty(gender), p => p.Gender == gender)
        //         .WhereIf(!string.IsNullOrEmpty(insurancePolicy), p => p.InsurancePolicy.Value == insurancePolicy)
        //         .WhereIf(!string.IsNullOrEmpty(placeOfWork), p => p.PlaceOfWork == placeOfWork)
        //         .WhereIf(precinct.HasValue, p => p.Precinct == precinct);
        // }
    }
}