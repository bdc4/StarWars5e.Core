﻿using NUnit.Framework;
using StarWars5e.Models.Enums;
using StarWars5e.Models.Monster;
using StarWars5e.Parser.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars5e.ParserTests.MonsterManual
{
    [TestFixture]
    //most of the tests are hardcoded just to test parsing algorithms with the 
    //apparent standard markdown patterns in the monster text.
    //modify mm_sample.txt with caution.
    public class MonsterManualProcessorTests : BaseTestSetup
    {
        private IBaseProcessor<Monster> _monsterProcessor;
        private List<string> _filesToParse;

        [SetUp]
        public void Setup()
        {
            _monsterProcessor = new MonsterProcessor();
            _filesToParse = new List<string> { "mm_sample.txt" };
        }

        [Test]
        public async Task ParsedSampleFile()
        {
           var result = await _monsterProcessor.Process(_filesToParse);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task ParsedSampleFile_AssertValues()
        {
            var monsterResult = (await _monsterProcessor.Process(_filesToParse)).First();

            Assert.AreEqual("Adolescent Acklay", monsterResult.Name);
            Assert.AreEqual(MonsterSize.Huge, Enum.Parse<MonsterSize>(monsterResult.Size));
            Assert.AreEqual("unaligned", monsterResult.Alignment);
            Assert.AreEqual(95, monsterResult.HitPoints);
            Assert.AreEqual(40, monsterResult.Speed);
            Assert.AreEqual(13, monsterResult.ArmorClass);
            Assert.AreEqual("natural armor", monsterResult.ArmorType);
            Assert.AreEqual(22, monsterResult.Strength);
            Assert.AreEqual(6, monsterResult.StrengthModifier);
            Assert.AreEqual(9, monsterResult.Dexterity);
            Assert.AreEqual(-1, monsterResult.DexterityModifier);
            Assert.AreEqual(17, monsterResult.Constitution);
            Assert.AreEqual(3, monsterResult.ConstitutionModifier);
            Assert.AreEqual(3, monsterResult.Intelligence);
            Assert.AreEqual(-4, monsterResult.IntelligenceModifier);
            Assert.AreEqual(11, monsterResult.Wisdom);
            Assert.AreEqual(0, monsterResult.WisdomModifier);
            Assert.AreEqual(5, monsterResult.Charisma);
            Assert.AreEqual(-3, monsterResult.CharismaModifier);
            Assert.Contains("—", monsterResult.Languages);
            Assert.AreEqual("5", monsterResult.ChallengeRating);
            Assert.Contains("passive Perception 10", monsterResult.Senses);
            Assert.AreEqual(3, monsterResult.Behaviors.Count);
            Assert.IsNotEmpty(monsterResult.ConditionImmunities);
            Assert.AreEqual("[\"Charmed\",\"Frightened\",\"Paralyzed\",\"Petrified\",\"Prone\",\"Restrained\",\"Stunned\"]"
                , monsterResult.ConditionImmunitiesParsedJson);
            Assert.IsEmpty(monsterResult.DamageResistances);
            Assert.AreEqual("", monsterResult.DamageResistancesParsedJson);
        }

        [Test]
        public async Task ParsedSampleFile_AssertBehaviors()
        {
            var monsterResult = (await _monsterProcessor.Process(_filesToParse)).First();

            var traitBehavior = monsterResult.Behaviors.FirstOrDefault(x => x.MonsterBehaviorTypeEnum == MonsterBehaviorType.Trait);
            
            Assert.NotNull(traitBehavior);
            Assert.IsNotEmpty(traitBehavior.Description);

            var actionBehavior = monsterResult.Behaviors.FirstOrDefault(x => x.MonsterBehaviorTypeEnum == MonsterBehaviorType.Action);

            Assert.AreEqual(AttackType.MeleeWeapon, actionBehavior.AttackTypeEnum);
            Assert.AreEqual("Bite", actionBehavior.Name);
            Assert.AreEqual(9, actionBehavior.AttackBonus);
            Assert.AreEqual("reach 5 ft.", actionBehavior.Range);
            Assert.AreEqual("one target.", actionBehavior.NumberOfTargets);
            Assert.AreEqual(DamageType.Kinetic, actionBehavior.DamageTypeEnum);
            Assert.AreEqual("4d8+6", actionBehavior.DamageRoll);
        }
    }
}