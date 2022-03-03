using MediatR;
using Newtonsoft.Json.Converters;
using TinyHelpers.Json.Serialization;
using TL.Api.Middleware;
using TL.CQRS;
using TL.Data;
using TL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new DateOnlyConverter()))
    .AddNewtonsoftJson(x => x.SerializerSettings.Converters.Add(new StringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TuneLibraryContext>();
builder.Services.AddTransient<ITuneRepository, TuneRepository>();
builder.Services.AddTransient<ITrackRepository, TrackRepository>();
builder.Services.AddTransient<IAlbumRepository, AlbumRepository>();
builder.Services.AddTransient<IArtistRepository, ArtistRepository>();
builder.Services.AddTransient<ITrackTuneRepository, TrackTuneRepository>();
builder.Services.AddTransient<ITrackAlbumUnitOfWork, TrackAlbumUnitOfWork>();
builder.Services.AddTransient<ITrackTuneUnitOfWork, TrackTuneUnitOfWork>();
builder.Services.AddTransient<IAlbumArtistUnitOfWork, AlbumArtistUnitOfWork>();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddMediatR(typeof(MediatorEntry).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();