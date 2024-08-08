namespace TopUpGenie.DataAccess.IntegrationTest.RepositoryTests;

public class RepositoryTests
{

    [Fact]
    public async Task Repository_Add_Entity_AddsSuccssfully()
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;
        var repository = new Repository<User>(dbContext, new NullLogger<Repository<User>>());
        var user = DependencyHelper.ValidUser;
        await repository.AddAsync(user);
        dbContext.SaveChanges();

        Assert.NotNull(user);
        Assert.True(user.Id > 0);
    }

    [Fact]
    public async Task Repository_Add_Invalid_Entity_Fails()
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;
        var repository = new Repository<User>(dbContext, new NullLogger<Repository<User>>());
        await repository.AddAsync(DependencyHelper.InValidUser);

        Assert.Throws<DbUpdateException>(() => dbContext.SaveChanges());
    }

    [Fact]
    public async Task Repository_GetAll_ReturnsAllEntities()
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;
        var repository = new Repository<User>(dbContext, new NullLogger<Repository<User>>());
        var entities = await repository.GetAllAsync();

        Assert.NotNull(entities);
        Assert.True(entities.Any());
    }

    [Fact]
    public async Task Repository_GetById_ReturnsOneEntity()
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;
        var repository = new Repository<User>(dbContext, new NullLogger<Repository<User>>());

        var entity = await repository.GetByIdAsync(1);
        Assert.NotNull(entity);
    }

    [Fact]
    public async Task Repository_Update_Entity_UpdatesSuccessfully()
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;
        var repository = new Repository<User>(dbContext, new NullLogger<Repository<User>>());
        var user = DependencyHelper.ValidUser;

        await repository.AddAsync(user);
        dbContext.SaveChanges();
        user.Name = "Updated-Name";

        repository.Update(user);
        dbContext.SaveChanges();

        var entity = await repository.GetByIdAsync(user.Id);
        
        Assert.NotNull(entity);
        Assert.True(user.Name == entity.Name);
    }

    [Fact]
    public async Task Repository_Update_Invalid_Entity_Fails()
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;

        var repository = new Repository<User>(dbContext, new NullLogger<Repository<User>>());

        var user = DependencyHelper.ValidUser;

        await repository.AddAsync(user);
        dbContext.SaveChanges();

        user.PhoneNumber = "0";
        repository.Update(user);

        await repository.AddAsync(DependencyHelper.InValidUser);
        Assert.Throws<DbUpdateException>(() => dbContext.SaveChanges());
    }

    [Fact]
    public async Task Repository_Delete_Entity_DeletesSuccssfully()
    {
        var dbContext = DependencyHelper.TopUpGenieDbContext;

        var repository = new Repository<User>(dbContext, new NullLogger<Repository<User>>());

        var entity = await repository.GetByIdAsync(5);

        repository.Delete(entity);
        dbContext.SaveChanges();

        entity = await repository.GetByIdAsync(5);

        Assert.Null(entity);
    }
}

