using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTFSManagerApi.Models
{
	public class GtfsContext : DbContext
	{
		public GtfsContext(DbContextOptions<GtfsContext> options)
			: base(options)
		{

		}

		public DbSet<AgencyItem> Agencies { get; set; }
		public DbSet<CalendarItem> Calendar { get; set; }
		public DbSet<RouteItem> Routes { get; set; }
		public DbSet<TripItem> Trips { get; set; }
		public DbSet<StopItem> Stops { get; set; }
		public DbSet<StoptimeItem> Stoptimes { get; set; }
	}

	public class AgencyItem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string agency_id { get; set; }
		[Required]
		public string agency_name { get; set; }
		[Required]
		public string agency_url { get; set; }
		[Required]
		public string agency_timezone { get; set; }
		public string agency_lang { get; set; }
		public string agency_phone { get; set; }
		public string agency_fare_url { get; set; }
		public string agency_email { get; set; }
	}

	public class CalendarItem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string primary_key { get; set; }
		[Required]
		public string service_id { get; set; }
		[Required]
		public string monday { get; set; }
		[Required]
		public string tuesday { get; set; }
		[Required]
		public string wednesday { get; set; }
		[Required]
		public string thursday { get; set; }
		[Required]
		public string friday { get; set; }
		[Required]
		public string saturday { get; set; }
		[Required]
		public string sunday { get; set; }
		[Required]
		public string start_date { get; set; }
		[Required]
		public string end_date { get; set; }
	}

	public class RouteItem
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string route_id { get; set; }
		public string agency_id { get; set; }
		[Required]
		public string route_short_name { get; set; }
		[Required]
		public string route_long_name { get; set; }
		public string route_desc { get; set; }
		[Required]
		public string route_type { get; set; }
		public string route_url { get; set; }
		public string route_color { get; set; }
		public string route_text_color { get; set; }
	}

	public class TripItem
	{
		[Required]
		[ForeignKey("RouteId")]
		public string route_id { get; set; }
		[Required]
		[ForeignKey("CalendarId")]
		public string service_id { get; set; }
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string trip_id { get; set; }
		public string trip_headsign { get; set; }
		public string trip_short_name { get; set; }
		public string direction_id { get; set; }
		public string block_id { get; set; }
		public string shape_id { get; set; }
		public string wheelchair_accessible { get; set; }
		public string bikes_allowed { get; set; }
	}

	public class StoptimeItem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string primary_key { get; set; }
		[Required]
		[ForeignKey("TripId")]
		public string trip_id { get; set; }
		[Required]
		public string arrival_time { get; set; }
		[Required]
		public string departure_time { get; set; }
		[Required]
		public string stop_id { get; set; }
		[Required]
		public string stop_sequence { get; set; }
		public string stop_headsign { get; set; }
		public string pickup_type { get; set; }
		public string drop_off_type { get; set; }
		public string shape_dist_traveled { get; set; }
		public string timepoint { get; set; }
	}

	public class StopItem
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string stop_id { get; set; }
		public string stop_code { get; set; }
		[Required]
		public string stop_name { get; set; }
		public string stop_desc { get; set; }
		[Required]
		public string stop_lat { get; set; }
		[Required]
		public string stop_lon { get; set; }
		public string zone_id { get; set; }
		public string stop_url { get; set; }
		public string location_type { get; set; }
		[ForeignKey("StopId")]
		public string parent_station { get; set; }
		public string stop_timezone { get; set; }
		public string wheelchair_boarding { get; set; }
	}
}
