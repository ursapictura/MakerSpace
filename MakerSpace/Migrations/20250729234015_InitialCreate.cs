using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MakerSpace.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IsSeller = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Libraries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patterns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MakerId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Published = table.Column<DateOnly>(type: "date", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Photo1 = table.Column<string>(type: "text", nullable: false),
                    Photo2 = table.Column<string>(type: "text", nullable: true),
                    Photo3 = table.Column<string>(type: "text", nullable: true),
                    Pdf = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patterns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patterns_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patterns_Users_MakerId",
                        column: x => x.MakerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patterns_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Storefronts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    BannerPhoto = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storefronts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storefronts_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoritePatterns",
                columns: table => new
                {
                    FavoritesId = table.Column<int>(type: "integer", nullable: false),
                    PatternId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritePatterns", x => new { x.FavoritesId, x.PatternId });
                    table.ForeignKey(
                        name: "FK_FavoritePatterns_Favorites_FavoritesId",
                        column: x => x.FavoritesId,
                        principalTable: "Favorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoritePatterns_Patterns_PatternId",
                        column: x => x.PatternId,
                        principalTable: "Patterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibraryPatterns",
                columns: table => new
                {
                    LibraryId = table.Column<int>(type: "integer", nullable: false),
                    PatternId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryPatterns", x => new { x.LibraryId, x.PatternId });
                    table.ForeignKey(
                        name: "FK_LibraryPatterns_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryPatterns_Patterns_PatternId",
                        column: x => x.PatternId,
                        principalTable: "Patterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatternTags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    PatternId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternTags", x => new { x.PatternId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PatternTags_Patterns_PatternId",
                        column: x => x.PatternId,
                        principalTable: "Patterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatternTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sewing" },
                    { 2, "Amigurumi" },
                    { 3, "Knit" },
                    { 4, "Quilting" },
                    { 5, "Embroidery" },
                    { 6, "Sashiko" },
                    { 7, "Crochet" },
                    { 8, "Weaving" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Clothing" },
                    { 2, "Toys" },
                    { 3, "Nautical" },
                    { 4, "SciFi" },
                    { 5, "Animals" },
                    { 6, "Fantasy" },
                    { 7, "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsSeller", "LastName", "UserName" },
                values: new object[,]
                {
                    { 1, "emily.johnson@example.com", "Emily", true, "Johnson", "sewingQueen" },
                    { 2, "mike.smith@example.com", "Michael", false, "Smith", "craftyMike" },
                    { 3, "sarah.williams@example.com", "Sarah", true, "Williams", "knitAndPearl" },
                    { 4, "dan.brown@example.com", "Daniel", false, "Brown", "diyDan" },
                    { 5, "laura.davis@example.com", "Laura", true, "Davis", "patternPro" }
                });

            migrationBuilder.InsertData(
                table: "Favorites",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Libraries",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Patterns",
                columns: new[] { "Id", "CategoryId", "Description", "IsApproved", "IsDeleted", "MakerId", "Name", "Pdf", "Photo1", "Photo2", "Photo3", "Price", "Published", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Not your grandma's kitchen apron, but it has the same vibe! Bake your favorite dishes while wearing this retro apron inspired by your coziest memories.", true, false, 1, "Kitchy Cooking Apron", "example/PdfPath/1", "example/path/fileName/1", "example/path/fileName/2", "example/path/fileName/3", 12.00m, new DateOnly(2025, 7, 1), null },
                    { 2, 2, "That's no moon....It's a Death Star! Recreate your favorite planet destroyer with this fun and simple amigurumi crochet pattern.", true, false, 2, "Crochet Death Star", "example/PdfPath/2", "example/path/fileName/4", "example/path/fileName/5", "example/path/fileName/6", 8.00m, new DateOnly(2025, 7, 2), null },
                    { 3, 3, "Snuggle up with a book and your favorite afternoon tea while wearing this cozy woll shawl.", true, false, 3, "Sunday Tea Time Shawl ", "example/PdfPath/3", "example/path/fileName/7", "example/path/fileName/8", "example/path/fileName/9", 8.00m, new DateOnly(2025, 7, 3), null },
                    { 4, 4, "A nautical inspired quilt block.", true, false, 4, "Octopus Quilt Block", "example/PdfPath/4", "example/path/fileName/10", "example/path/fileName/11", "example/path/fileName/12", 5.00m, new DateOnly(2025, 7, 4), null },
                    { 5, 5, "Create your own custome fairy garden embroidery with flowers, mushrooms, and a tiny fairy cottage! This pattern is suitable for 8 inch hoops.", true, false, 5, "Fairy Garden Embroidery", "example/PdfPath/5", "example/path/fileName/113", "example/path/fileName/14", "example/path/fileName/15", 13.00m, new DateOnly(2025, 7, 5), null }
                });

            migrationBuilder.InsertData(
                table: "Storefronts",
                columns: new[] { "Id", "BannerPhoto", "Description", "Name", "SellerId" },
                values: new object[,]
                {
                    { 1, "https://d2culxnxbccemt.cloudfront.net/knit/content/uploads/2025/05/23175938/banner-home-patterns.jpg", "Unique, hand-crafted sewing patterns by Emily Johnson.", "Emily's Designs", 1 },
                    { 2, "https://images.ctfassets.net/ivgal0r4n3ub/5fmLOvNNA3YIKYdJsv8NVV/c999033ed0e21c867162d43cb3ad800a/Knitting_Banner.jpg", "Trendy knitting patterns for all skill levels.", "Knit & Pearl Boutique", 3 },
                    { 3, "https://images.squarespace-cdn.com/content/v1/5ddee0943d393a7e06173756/1674234278148-SZWPAT670GIYS1US0YGQ/Blog+Post+Banner+Journal+in+Yarn+How+to+Plan+a+Temperature+Blanket+Knitting+Project.png?format=1000w", "Professional-grade patterns for serious hobbyists.", "Laura's Pattern Pro", 5 },
                    { 4, "https://images.squarespace-cdn.com/content/v1/5ddee0943d393a7e06173756/1674666723668-EYEKOQRTYN7BEL68RXK3/Blog+Post+Banner+Temperature+Blanket+How+to+Knit+in+Garter+Stitch+Fifty+Four+Ten+Studio+Worsted+Yarn+2023.png?format=1000w", "Exclusive marketplace items curated by Mike.", "Crafty Mike's Market", 2 },
                    { 5, "https://previews.123rf.com/images/irishasel/irishasel1808/irishasel180800506/107485960-long-banner-a-cute-baby-lies-on-a-basket-with-tangles-of-knitting-threads-hendmeid-of-multi.jpg", "DIY patterns and tools for beginners and pros alike.", "DIY Dan's Workshop", 4 }
                });

            migrationBuilder.InsertData(
                table: "FavoritePatterns",
                columns: new[] { "FavoritesId", "PatternId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "LibraryPatterns",
                columns: new[] { "LibraryId", "PatternId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "PatternTags",
                columns: new[] { "PatternId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 2, 4 },
                    { 3, 1 },
                    { 4, 3 },
                    { 4, 5 },
                    { 5, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePatterns_PatternId",
                table: "FavoritePatterns",
                column: "PatternId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryPatterns_PatternId",
                table: "LibraryPatterns",
                column: "PatternId");

            migrationBuilder.CreateIndex(
                name: "IX_Patterns_CategoryId",
                table: "Patterns",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Patterns_MakerId",
                table: "Patterns",
                column: "MakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Patterns_UserId",
                table: "Patterns",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternTags_TagId",
                table: "PatternTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Storefronts_SellerId",
                table: "Storefronts",
                column: "SellerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoritePatterns");

            migrationBuilder.DropTable(
                name: "LibraryPatterns");

            migrationBuilder.DropTable(
                name: "PatternTags");

            migrationBuilder.DropTable(
                name: "Storefronts");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropTable(
                name: "Patterns");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
