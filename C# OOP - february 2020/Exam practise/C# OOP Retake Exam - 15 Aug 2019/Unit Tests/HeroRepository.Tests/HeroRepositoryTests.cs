using System;
using NUnit.Framework;

public class HeroRepositoryTests
{
    [Test]
    public void Hero_Constructor_Work_Properly()
    {
        Hero hero = new Hero("Ivan", 2);
        Assert.AreEqual("Ivan", hero.Name);
    }
    [Test]
    public void Hero_Constructor_Work_Properly2()
    {
        Hero hero = new Hero("Ivan", 2);
        Assert.AreEqual(2, hero.Level);
    }

    [Test]
    public void Hero_Create_Throw_Exception_When_Null()
    {
        HeroRepository heros = new HeroRepository();
       Hero hero = null;
        Assert.Throws<ArgumentNullException>(() => heros.Create(hero));
    }

    [Test]
    public void Hero_Create_Throw_Exception_When_Hero_Exist()
    {
        HeroRepository heros = new HeroRepository();
        var her = new Hero("Ivan", 2);
        heros.Create(her);
        Assert.Throws<InvalidOperationException>(() =>heros.Create(her));
    }

    [Test]
    public void Hero_Create_OK()
    {
        HeroRepository heros = new HeroRepository();
        var her = new Hero("Ivan", 2);
        heros.Create(her);
        Assert.AreEqual(1, heros.Heroes.Count);
    }
    [Test]
    public void Hero_Remove_Throw_Exception_When_Null()
    {
        HeroRepository heros = new HeroRepository();
        
        Assert.Throws<ArgumentNullException>(() => heros.Remove(null));
    }
    [Test]
    public void Hero_Remove_OK()
    {
        HeroRepository heros = new HeroRepository();
        var her = new Hero("Ivan", 2);
        heros.Create(her);
        Assert.IsTrue(heros.Remove("Ivan"));
    }

    [Test]
    public void Hero_With_Higest_Lvl_OK()
    {
        HeroRepository heros = new HeroRepository();
        var hero = new Hero("Ivan", 2);
        var hero2 = new Hero("Petar", 5);

        Assert.Greater(hero2.Level,hero.Level);
    }

    [Test]
    public void Hero_GetHero_OK()
    {
        HeroRepository heros = new HeroRepository();
        var hero = new Hero("Ivan", 2);
        var hero2 = new Hero("Petar", 5);
        heros.Create(hero);
        heros.Create(hero2);
        var getHero = heros.GetHero("Ivan");
        Assert.AreEqual(hero.Name, getHero.Name);
    }
}