﻿using System;
using System.Collections.Generic;
using System.Linq;
using BPlusTree;
using BPlusTree.Blocks;
using BPlusTree.Writables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresUnitTest
{
    public static class Utils
    {
        public static int RandomDataBlockSize = 10;

        public static string Print<T>(this IEnumerable<T> enumerable) => $"[{string.Join(", ", enumerable)}]";

        public static string RandomString(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var str = Enumerable.Repeat(chars, length).Select(ch => ch[random.Next(0, chars.Length)]).ToArray();
            return new string(str);
        }

        public static DateTime RandomDateTime(Random random) => new DateTime(random.Next(1900, 2000), random.Next(1, 13), random.Next(1, 25));

        public static Patient RandomPatient(Random random)
        {
            var patient = new Patient
            {
                FirstName = RandomString(random, random.Next(10, 25)),
                LastName = RandomString(random, random.Next(10, 25)),
                Birthday = RandomDateTime(random),
                CardId = random.Next()
            };
            var numHospitalizations = random.Next(10, 100);
            for (var i = 0; i < numHospitalizations; i++)
            {
                patient.Hospitalizations.Add(new Hospitalization
                {
                    Start = RandomDateTime(random),
                    End = RandomDateTime(random),
                    Diagnosis = RandomString(random, random.Next(10, 40))
                });
            }
            return patient;
        }

        public static void AssertPatients(Patient p1, Patient p2)
        {
            Assert.AreEqual(p1.FirstName, p2.FirstName);
            Assert.AreEqual(p1.LastName, p2.LastName);
            Assert.AreEqual(p1.Birthday, p2.Birthday);
            Assert.AreEqual(p1.CardId, p2.CardId);
            var p1Hospitalizations = p1.Hospitalizations.Where(hosp => hosp != null).ToArray();
            var p2Hospitalizations = p2.Hospitalizations.Where(hosp => hosp != null).ToArray();
            Assert.AreEqual(p1Hospitalizations.Length, p2Hospitalizations.Length);
            for (var i = 0; i < p1Hospitalizations.Length; i++)
            {
                var p1Hospitalization = p1Hospitalizations[i];
                var p2Hospitalization = p2Hospitalizations[i];
                Assert.AreEqual(p1Hospitalization.Start, p2Hospitalization.Start);
                Assert.AreEqual(p1Hospitalization.End, p2Hospitalization.End);
                Assert.AreEqual(p1Hospitalization.Diagnosis, p2Hospitalization.Diagnosis);
            }
        }

        public static DataBlock<WritableInt, Patient> RandomDataBlock(Random random)
        {
            var block = new DataBlock<WritableInt, Patient>(RandomDataBlockSize);
            var count = random.Next(1, block.MaxSize);
            for (var i = 0; i < count; i++)
                block.Insert(new WritableInt(i), RandomPatient(random));
            return block;
        }

        public static void AssertDataBlocks(DataBlock<WritableInt, Patient> b1, DataBlock<WritableInt, Patient> b2)
        {
            var b1Patients = b1.ToArray();
            var b2Patients = b2.ToArray();
            Assert.AreEqual(b1Patients.Length, b2Patients.Length);
            for (var i = 0; i < b1Patients.Length; i++)
            {
                var p1 = b1Patients[i];
                var p2 = b2Patients[i];
                AssertPatients(p1, p2);
            }
        }

        public static List<WritableInt> RandomUniqueList(Random rand, int count)
        {
            var list = new List<WritableInt>(count);
            for (var i = 0; i < count; i++)
                list.Add(new WritableInt(i));
            list.Sort((i1, i2) => rand.Next());
            return list;
        }

        public static List<Patient> RandomUniquePatients(Random rand, int count)
        {
            var list = new List<Patient>(count);
            for (var i = 0; i < count; i++)
            {
                var patient = RandomPatient(rand);
                patient.CardId = i;
                list.Add(patient);
            }
            list.Sort((i1, i2) => rand.Next());
            return list;
        }
    }
}
