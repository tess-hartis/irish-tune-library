﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TL.Data;

#nullable disable

namespace TL.Data.Migrations
{
    [DbContext(typeof(TuneLibraryContext))]
    [Migration("20220218220703_enumValObj")]
    partial class enumValObj
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AlbumArtist", b =>
                {
                    b.Property<int>("AlbumsId")
                        .HasColumnType("integer");

                    b.Property<int>("ArtistsId")
                        .HasColumnType("integer");

                    b.HasKey("AlbumsId", "ArtistsId");

                    b.HasIndex("ArtistsId");

                    b.ToTable("AlbumArtist");
                });

            modelBuilder.Entity("TL.Domain.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("TL.Domain.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("TL.Domain.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("integer");

                    b.Property<int?>("TuneId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("TuneId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("TL.Domain.TrackTune", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TrackId")
                        .HasColumnType("integer");

                    b.Property<int>("TuneId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.HasIndex("TuneId");

                    b.ToTable("TrackTunes");
                });

            modelBuilder.Entity("TL.Domain.Tune", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DateAdded")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tunes");
                });

            modelBuilder.Entity("AlbumArtist", b =>
                {
                    b.HasOne("TL.Domain.Album", null)
                        .WithMany()
                        .HasForeignKey("AlbumsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TL.Domain.Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TL.Domain.Album", b =>
                {
                    b.OwnsOne("TL.Domain.ValueObjects.AlbumValueObjects.AlbumTitle", "Title", b1 =>
                        {
                            b1.Property<int>("AlbumId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title");

                            b1.HasKey("AlbumId");

                            b1.ToTable("Albums");

                            b1.WithOwner()
                                .HasForeignKey("AlbumId");
                        });

                    b.OwnsOne("TL.Domain.ValueObjects.AlbumValueObjects.AlbumYear", "Year", b1 =>
                        {
                            b1.Property<int>("AlbumId")
                                .HasColumnType("integer");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("Year");

                            b1.HasKey("AlbumId");

                            b1.ToTable("Albums");

                            b1.WithOwner()
                                .HasForeignKey("AlbumId");
                        });

                    b.Navigation("Title")
                        .IsRequired();

                    b.Navigation("Year")
                        .IsRequired();
                });

            modelBuilder.Entity("TL.Domain.Artist", b =>
                {
                    b.OwnsOne("TL.Domain.ValueObjects.ArtistValueObjects.ArtistName", "Name", b1 =>
                        {
                            b1.Property<int>("ArtistId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Name");

                            b1.HasKey("ArtistId");

                            b1.ToTable("Artists");

                            b1.WithOwner()
                                .HasForeignKey("ArtistId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("TL.Domain.Track", b =>
                {
                    b.HasOne("TL.Domain.Album", "Album")
                        .WithMany("TrackListing")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TL.Domain.Tune", null)
                        .WithMany("FeaturedOnTrack")
                        .HasForeignKey("TuneId");

                    b.OwnsOne("TL.Domain.ValueObjects.TrackValueObjects.TrackTitle", "Title", b1 =>
                        {
                            b1.Property<int>("TrackId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title");

                            b1.HasKey("TrackId");

                            b1.ToTable("Tracks");

                            b1.WithOwner()
                                .HasForeignKey("TrackId");
                        });

                    b.OwnsOne("TL.Domain.ValueObjects.TrackValueObjects.TrkNumber", "TrkNumber", b1 =>
                        {
                            b1.Property<int>("TrackId")
                                .HasColumnType("integer");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("TrackNumber");

                            b1.HasKey("TrackId");

                            b1.ToTable("Tracks");

                            b1.WithOwner()
                                .HasForeignKey("TrackId");
                        });

                    b.Navigation("Album");

                    b.Navigation("Title")
                        .IsRequired();

                    b.Navigation("TrkNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("TL.Domain.TrackTune", b =>
                {
                    b.HasOne("TL.Domain.Track", "Track")
                        .WithMany("TrackTunes")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TL.Domain.Tune", "Tune")
                        .WithMany()
                        .HasForeignKey("TuneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("TL.Domain.ValueObjects.TrackTuneValueObjects.TrackTuneOrder", "Order", b1 =>
                        {
                            b1.Property<int>("TrackTuneId")
                                .HasColumnType("integer");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("Order");

                            b1.HasKey("TrackTuneId");

                            b1.ToTable("TrackTunes");

                            b1.WithOwner()
                                .HasForeignKey("TrackTuneId");
                        });

                    b.Navigation("Order")
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("Tune");
                });

            modelBuilder.Entity("TL.Domain.Tune", b =>
                {
                    b.OwnsMany("TL.Domain.ValueObjects.TuneValueObjects.TuneTitle", "AlternateTitles", b1 =>
                        {
                            b1.Property<int>("TuneId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title");

                            b1.HasKey("TuneId", "Id");

                            b1.ToTable("Tunes_AlternateTitles");

                            b1.WithOwner()
                                .HasForeignKey("TuneId");
                        });

                    b.OwnsOne("TL.Domain.ValueObjects.TuneValueObjects.TuneTitle", "Title", b1 =>
                        {
                            b1.Property<int>("TuneId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title");

                            b1.HasKey("TuneId");

                            b1.ToTable("Tunes");

                            b1.WithOwner()
                                .HasForeignKey("TuneId");
                        });

                    b.OwnsOne("TL.Domain.ValueObjects.TuneValueObjects.TuneComposer", "Composer", b1 =>
                        {
                            b1.Property<int>("TuneId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Composer");

                            b1.HasKey("TuneId");

                            b1.ToTable("Tunes");

                            b1.WithOwner()
                                .HasForeignKey("TuneId");
                        });

                    b.OwnsOne("TL.Domain.ValueObjects.TuneValueObjects.TuneKeyValueObj", "TuneKey", b1 =>
                        {
                            b1.Property<int>("TuneId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Key");

                            b1.HasKey("TuneId");

                            b1.ToTable("Tunes");

                            b1.WithOwner()
                                .HasForeignKey("TuneId");
                        });

                    b.OwnsOne("TL.Domain.ValueObjects.TuneValueObjects.TuneTypeValueObj", "TuneType", b1 =>
                        {
                            b1.Property<int>("TuneId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Type");

                            b1.HasKey("TuneId");

                            b1.ToTable("Tunes");

                            b1.WithOwner()
                                .HasForeignKey("TuneId");
                        });

                    b.Navigation("AlternateTitles");

                    b.Navigation("Composer")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();

                    b.Navigation("TuneKey")
                        .IsRequired();

                    b.Navigation("TuneType")
                        .IsRequired();
                });

            modelBuilder.Entity("TL.Domain.Album", b =>
                {
                    b.Navigation("TrackListing");
                });

            modelBuilder.Entity("TL.Domain.Track", b =>
                {
                    b.Navigation("TrackTunes");
                });

            modelBuilder.Entity("TL.Domain.Tune", b =>
                {
                    b.Navigation("FeaturedOnTrack");
                });
#pragma warning restore 612, 618
        }
    }
}
