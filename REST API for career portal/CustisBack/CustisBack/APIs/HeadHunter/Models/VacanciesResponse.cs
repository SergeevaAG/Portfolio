using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CustisCareers.APIs.HeadHunter.Models
{
    public class Department
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Area
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Salary
    {
        [JsonPropertyName("from")]
        public int? From { get; set; }

        [JsonPropertyName("to")]
        public int? To { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("gross")]
        public bool Gross { get; set; }
    }

    public class Type
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Metro
    {
        [JsonPropertyName("station_name")]
        public string StationName { get; set; }

        [JsonPropertyName("line_name")]
        public string LineName { get; set; }

        [JsonPropertyName("station_id")]
        public string StationId { get; set; }

        [JsonPropertyName("line_id")]
        public string LineId { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }

    public class MetroStation
    {
        [JsonPropertyName("station_name")]
        public string StationName { get; set; }

        [JsonPropertyName("line_name")]
        public string LineName { get; set; }

        [JsonPropertyName("station_id")]
        public string StationId { get; set; }

        [JsonPropertyName("line_id")]
        public string LineId { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("building")]
        public string Building { get; set; }

        [JsonPropertyName("description")]
        public object Description { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }

        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("metro")]
        public Metro Metro { get; set; }

        [JsonPropertyName("metro_stations")]
        public List<MetroStation> MetroStations { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class LogoUrls
    {
        [JsonPropertyName("240")]
        public string _240 { get; set; }

        [JsonPropertyName("90")]
        public string _90 { get; set; }

        [JsonPropertyName("original")]
        public string Original { get; set; }
    }

    public class Employer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("alternate_url")]
        public string AlternateUrl { get; set; }

        [JsonPropertyName("logo_urls")]
        public LogoUrls LogoUrls { get; set; }

        [JsonPropertyName("vacancies_url")]
        public string VacanciesUrl { get; set; }

        [JsonPropertyName("trusted")]
        public bool Trusted { get; set; }
    }

    public class Snippet
    {
        [JsonPropertyName("requirement")]
        public string Requirement { get; set; }

        [JsonPropertyName("responsibility")]
        public string Responsibility { get; set; }
    }

    public class Phone
    {
        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }

    public class Contacts
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phones")]
        public List<Phone> Phones { get; set; }
    }

    public class Schedule
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class WorkingTimeInterval
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Vacancy
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("premium")]
        public bool Premium { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("department")]
        public Department Department { get; set; }

        [JsonPropertyName("has_test")]
        public bool HasTest { get; set; }

        [JsonPropertyName("response_letter_required")]
        public bool ResponseLetterRequired { get; set; }

        [JsonPropertyName("area")]
        public Area Area { get; set; }

        [JsonPropertyName("salary")]
        public Salary Salary { get; set; }

        [JsonPropertyName("type")]
        public Type Type { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("response_url")]
        public object ResponseUrl { get; set; }

        [JsonPropertyName("sort_point_distance")]
        public object SortPointDistance { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("archived")]
        public bool Archived { get; set; }

        [JsonPropertyName("apply_alternate_url")]
        public string ApplyAlternateUrl { get; set; }

        [JsonPropertyName("insider_interview")]
        public object InsiderInterview { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("alternate_url")]
        public string AlternateUrl { get; set; }

        [JsonPropertyName("relations")]
        public List<object> Relations { get; set; }

        [JsonPropertyName("employer")]
        public Employer Employer { get; set; }

        [JsonPropertyName("snippet")]
        public Snippet Snippet { get; set; }

        [JsonPropertyName("contacts")]
        public Contacts Contacts { get; set; }

        [JsonPropertyName("schedule")]
        public Schedule Schedule { get; set; }

        [JsonPropertyName("working_days")]
        public List<object> WorkingDays { get; set; }

        [JsonPropertyName("working_time_intervals")]
        public List<WorkingTimeInterval> WorkingTimeIntervals { get; set; }

        [JsonPropertyName("working_time_modes")]
        public List<object> WorkingTimeModes { get; set; }

        [JsonPropertyName("accept_temporary")]
        public bool AcceptTemporary { get; set; }
    }

    public class VacanciesResponse
    {
        [JsonPropertyName("items")]
        public List<Vacancy> Items { get; set; }

        [JsonPropertyName("found")]
        public int Found { get; set; }

        [JsonPropertyName("pages")]
        public int Pages { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("clusters")]
        public object Clusters { get; set; }

        [JsonPropertyName("arguments")]
        public object Arguments { get; set; }

        [JsonPropertyName("alternate_url")]
        public string AlternateUrl { get; set; }
    }
}