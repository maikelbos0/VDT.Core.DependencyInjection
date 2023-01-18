﻿using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrenceTests {
        private class TestRecurrencePattern : RecurrencePattern {
            public HashSet<DateTime> ValidDates { get; } = new();

            public TestRecurrencePattern(int interval, DateTime referenceDate) : base(interval, referenceDate) { }

            public override bool IsValid(DateTime date) => ValidDates.Contains(date);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void CacheDates(bool cacheDates, bool expectedCacheDates) {
            var recurrence = new Recurrence(
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 11),
                null,
                cacheDates
            );

            Assert.Equal(expectedCacheDates, recurrence.CacheDates);
        }

        [Fact]
        public void GetDates_No_Pattern() {
            var recurrence = new Recurrence(
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 11),
                null,
                false
            );

            var dates = recurrence.GetDates();

            Assert.Empty(dates);
        }

        [Fact]
        public void GetDates_Single_Pattern() {
            var recurrence = new Recurrence(
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 4),
                null,
                false,
                new DailyRecurrencePattern(2, new DateTime(2022, 1, 1))
            );

            var dates = recurrence.GetDates();

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 3)
            }, dates);
        }

        [Fact]
        public void GetDates_Double_Pattern() {
            var recurrence = new Recurrence(
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 4),
                null,
                false,
                new DailyRecurrencePattern(2, new DateTime(2022, 1, 1)),
                new DailyRecurrencePattern(5, new DateTime(2022, 1, 4))
            );

            var dates = recurrence.GetDates();

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 3),
                new DateTime(2022, 1, 4)
            }, dates);
        }

        [Fact]
        public void GetDates_From_To_Outside_StartDate_EndDate() {
            var recurrence = new Recurrence(
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 4),
                null,
                false,
                new DailyRecurrencePattern(2, new DateTime(2022, 1, 1))
            );

            var dates = recurrence.GetDates(DateTime.MinValue, DateTime.MaxValue);

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 3)
            }, dates);
        }

        [Fact]
        public void GetDates_From_To_Inside_StartDate_EndDate() {
            var recurrence = new Recurrence(
                DateTime.MinValue,
                DateTime.MaxValue,
                null,
                false,
                new DailyRecurrencePattern(2, new DateTime(2022, 1, 1))
            );

            var dates = recurrence.GetDates(new DateTime(2022, 1, 1), new DateTime(2022, 1, 4));

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 3)
            }, dates);
        }

        [Fact]
        public void GetDates_Offset_ReferenceDate() {
            var recurrence = new Recurrence(
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 4),
                null,
                false,
                new DailyRecurrencePattern(2, new DateTime(2022, 1, 2))
            );

            var dates = recurrence.GetDates();

            Assert.Equal(new[] {
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 4)
            }, dates);
        }

        [Fact]
        public void GetDates_Occurrences() {
            var recurrence = new Recurrence(
                new DateTime(2022, 1, 1),
                DateTime.MaxValue,
                2,
                false,
                new DailyRecurrencePattern(2, new DateTime(2022, 1, 1))
            );

            var dates = recurrence.GetDates();

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 3)
            }, dates);
        }

        [Fact]
        public void GetDates_Occurrences_From_After_StartDate() {
            var recurrence = new Recurrence(
                new DateTime(2022, 1, 1),
                DateTime.MaxValue,
                5,
                false,
                new DailyRecurrencePattern(2, new DateTime(2022, 1, 1))
            );

            var dates = recurrence.GetDates(new DateTime(2022, 1, 6));

            Assert.Equal(new[] {
                new DateTime(2022, 1, 7),
                new DateTime(2022, 1, 9)
            }, dates);
        }

        [Theory]
        [InlineData("2022-01-01", true)]
        [InlineData("2022-01-02", false)]
        [InlineData("2022-01-03", true)]
        [InlineData("2022-01-04", true)]
        [InlineData("2022-01-05", true)]
        [InlineData("2022-01-06", false)]
        public void IsValidInAnyPattern(DateTime date, bool expectedIsValid) {
            var recurrence = new Recurrence(
                DateTime.MinValue,
                DateTime.MaxValue,
                null,
                false,
                new DailyRecurrencePattern(2, new DateTime(2022, 1, 1)),
                new DailyRecurrencePattern(3, new DateTime(2022, 1, 1))
            );

            Assert.Equal(expectedIsValid, recurrence.IsValidInAnyPattern(date));
        }

        [Fact]
        public void IsValidInAnyPattern_Caches_When_CacheDates_Is_True() {
            var recurrencePattern = new TestRecurrencePattern(2, new DateTime(2022, 1, 1));
            var recurrence = new Recurrence(DateTime.MinValue, DateTime.MaxValue, null, true, recurrencePattern);
            
            var firstResult = recurrence.IsValidInAnyPattern(new DateTime(2022, 1, 1));

            recurrencePattern.ValidDates.Add(new DateTime(2022, 1, 1));

            Assert.Equal(firstResult, recurrence.IsValidInAnyPattern(new DateTime(2022, 1, 1)));
        }

        [Fact]
        public void IsValidInAnyPattern_Does_Not_Cache_When_CacheDates_Is_False() {
            var recurrencePattern = new TestRecurrencePattern(2, new DateTime(2022, 1, 1));
            var recurrence = new Recurrence(DateTime.MinValue, DateTime.MaxValue, null, false, recurrencePattern);

            var firstResult = recurrence.IsValidInAnyPattern(new DateTime(2022, 1, 1));

            recurrencePattern.ValidDates.Add(new DateTime(2022, 1, 1));

            Assert.NotEqual(firstResult, recurrence.IsValidInAnyPattern(new DateTime(2022, 1, 1)));
        }
    }
}
