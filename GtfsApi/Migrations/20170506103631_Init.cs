using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GtfsApi.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    agency_id = table.Column<string>(nullable: false),
                    agency_email = table.Column<string>(nullable: true),
                    agency_fare_url = table.Column<string>(nullable: true),
                    agency_lang = table.Column<string>(nullable: true),
                    agency_name = table.Column<string>(nullable: false),
                    agency_phone = table.Column<string>(nullable: true),
                    agency_timezone = table.Column<string>(nullable: false),
                    agency_url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.agency_id);
                });

            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    primary_key = table.Column<string>(nullable: false),
                    end_date = table.Column<string>(nullable: false),
                    friday = table.Column<string>(nullable: false),
                    monday = table.Column<string>(nullable: false),
                    saturday = table.Column<string>(nullable: false),
                    service_id = table.Column<string>(nullable: false),
                    start_date = table.Column<string>(nullable: false),
                    sunday = table.Column<string>(nullable: false),
                    thursday = table.Column<string>(nullable: false),
                    tuesday = table.Column<string>(nullable: false),
                    wednesday = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.primary_key);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    route_id = table.Column<string>(nullable: false),
                    agency_id = table.Column<string>(nullable: true),
                    route_color = table.Column<string>(nullable: true),
                    route_desc = table.Column<string>(nullable: true),
                    route_long_name = table.Column<string>(nullable: false),
                    route_short_name = table.Column<string>(nullable: false),
                    route_text_color = table.Column<string>(nullable: true),
                    route_type = table.Column<string>(nullable: false),
                    route_url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.route_id);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    stop_id = table.Column<string>(nullable: false),
                    location_type = table.Column<string>(nullable: true),
                    parent_station = table.Column<string>(nullable: true),
                    stop_code = table.Column<string>(nullable: true),
                    stop_desc = table.Column<string>(nullable: true),
                    stop_lat = table.Column<string>(nullable: false),
                    stop_lon = table.Column<string>(nullable: false),
                    stop_name = table.Column<string>(nullable: false),
                    stop_timezone = table.Column<string>(nullable: true),
                    stop_url = table.Column<string>(nullable: true),
                    wheelchair_boarding = table.Column<string>(nullable: true),
                    zone_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.stop_id);
                });

            migrationBuilder.CreateTable(
                name: "Stoptimes",
                columns: table => new
                {
                    primary_key = table.Column<string>(nullable: false),
                    arrival_time = table.Column<string>(nullable: false),
                    departure_time = table.Column<string>(nullable: false),
                    drop_off_type = table.Column<string>(nullable: true),
                    pickup_type = table.Column<string>(nullable: true),
                    shape_dist_traveled = table.Column<string>(nullable: true),
                    stop_headsign = table.Column<string>(nullable: true),
                    stop_id = table.Column<string>(nullable: false),
                    stop_sequence = table.Column<string>(nullable: false),
                    timepoint = table.Column<string>(nullable: true),
                    trip_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stoptimes", x => x.primary_key);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    trip_id = table.Column<string>(nullable: false),
                    bikes_allowed = table.Column<string>(nullable: true),
                    block_id = table.Column<string>(nullable: true),
                    direction_id = table.Column<string>(nullable: true),
                    route_id = table.Column<string>(nullable: false),
                    service_id = table.Column<string>(nullable: false),
                    shape_id = table.Column<string>(nullable: true),
                    trip_headsign = table.Column<string>(nullable: true),
                    trip_short_name = table.Column<string>(nullable: true),
                    wheelchair_accessible = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.trip_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Stoptimes");

            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
