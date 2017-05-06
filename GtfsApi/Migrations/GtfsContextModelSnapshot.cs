using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GTFSManagerApi.Models;

namespace GtfsApi.Migrations
{
    [DbContext(typeof(GtfsContext))]
    partial class GtfsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GTFSManagerApi.Models.AgencyItem", b =>
                {
                    b.Property<string>("agency_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("agency_email");

                    b.Property<string>("agency_fare_url");

                    b.Property<string>("agency_lang");

                    b.Property<string>("agency_name")
                        .IsRequired();

                    b.Property<string>("agency_phone");

                    b.Property<string>("agency_timezone")
                        .IsRequired();

                    b.Property<string>("agency_url")
                        .IsRequired();

                    b.HasKey("agency_id");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("GTFSManagerApi.Models.CalendarItem", b =>
                {
                    b.Property<string>("primary_key")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("end_date")
                        .IsRequired();

                    b.Property<string>("friday")
                        .IsRequired();

                    b.Property<string>("monday")
                        .IsRequired();

                    b.Property<string>("saturday")
                        .IsRequired();

                    b.Property<string>("service_id")
                        .IsRequired();

                    b.Property<string>("start_date")
                        .IsRequired();

                    b.Property<string>("sunday")
                        .IsRequired();

                    b.Property<string>("thursday")
                        .IsRequired();

                    b.Property<string>("tuesday")
                        .IsRequired();

                    b.Property<string>("wednesday")
                        .IsRequired();

                    b.HasKey("primary_key");

                    b.ToTable("Calendar");
                });

            modelBuilder.Entity("GTFSManagerApi.Models.RouteItem", b =>
                {
                    b.Property<string>("route_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("agency_id");

                    b.Property<string>("route_color");

                    b.Property<string>("route_desc");

                    b.Property<string>("route_long_name")
                        .IsRequired();

                    b.Property<string>("route_short_name")
                        .IsRequired();

                    b.Property<string>("route_text_color");

                    b.Property<string>("route_type")
                        .IsRequired();

                    b.Property<string>("route_url");

                    b.HasKey("route_id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("GTFSManagerApi.Models.StopItem", b =>
                {
                    b.Property<string>("stop_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("location_type");

                    b.Property<string>("parent_station");

                    b.Property<string>("stop_code");

                    b.Property<string>("stop_desc");

                    b.Property<string>("stop_lat")
                        .IsRequired();

                    b.Property<string>("stop_lon")
                        .IsRequired();

                    b.Property<string>("stop_name")
                        .IsRequired();

                    b.Property<string>("stop_timezone");

                    b.Property<string>("stop_url");

                    b.Property<string>("wheelchair_boarding");

                    b.Property<string>("zone_id");

                    b.HasKey("stop_id");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("GTFSManagerApi.Models.StoptimeItem", b =>
                {
                    b.Property<string>("primary_key")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("arrival_time")
                        .IsRequired();

                    b.Property<string>("departure_time")
                        .IsRequired();

                    b.Property<string>("drop_off_type");

                    b.Property<string>("pickup_type");

                    b.Property<string>("shape_dist_traveled");

                    b.Property<string>("stop_headsign");

                    b.Property<string>("stop_id")
                        .IsRequired();

                    b.Property<string>("stop_sequence")
                        .IsRequired();

                    b.Property<string>("timepoint");

                    b.Property<string>("trip_id")
                        .IsRequired();

                    b.HasKey("primary_key");

                    b.ToTable("Stoptimes");
                });

            modelBuilder.Entity("GTFSManagerApi.Models.TripItem", b =>
                {
                    b.Property<string>("trip_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("bikes_allowed");

                    b.Property<string>("block_id");

                    b.Property<string>("direction_id");

                    b.Property<string>("route_id")
                        .IsRequired();

                    b.Property<string>("service_id")
                        .IsRequired();

                    b.Property<string>("shape_id");

                    b.Property<string>("trip_headsign");

                    b.Property<string>("trip_short_name");

                    b.Property<string>("wheelchair_accessible");

                    b.HasKey("trip_id");

                    b.ToTable("Trips");
                });
        }
    }
}
