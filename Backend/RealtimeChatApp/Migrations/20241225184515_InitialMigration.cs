using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealtimeChatApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentimentAnalysisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentimentAnalysis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sentiment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfidenceScoresPositive = table.Column<double>(type: "float", nullable: false),
                    ConfidenceScoresNegative = table.Column<double>(type: "float", nullable: false),
                    ConfidenceScoresNeutral = table.Column<double>(type: "float", nullable: false),
                    AnalyzedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentimentAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SentimentAnalysis_ChatMessages_ChatMessageId",
                        column: x => x.ChatMessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SentimentAnalysis_ChatMessageId",
                table: "SentimentAnalysis",
                column: "ChatMessageId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SentimentAnalysis");

            migrationBuilder.DropTable(
                name: "ChatMessages");
        }
    }
}
