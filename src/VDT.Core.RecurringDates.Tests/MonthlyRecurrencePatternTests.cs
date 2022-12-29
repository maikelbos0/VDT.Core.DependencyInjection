﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class MonthlyRecurrencePatternTests {
        [Theory]
        [InlineData(2022, 1, 1, 28, 29, 30, 31)]
        [InlineData(2022, 4, 1, 28, 29, 30)]
        [InlineData(2020, 2, 1, 28, 29)]
        [InlineData(2022, 2, 1, 28)]
        public void GetDaysOfMonth_DaysOfMonth(int year, int month, params int[] expectedDays) {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfMonth = new HashSet<int>() { 1, 28, 29, 30, 31 }
            };

            var result = pattern.GetDaysOfMonth(new DateTime(year, month, 1));

            Assert.Equal(expectedDays.ToHashSet(), result);
        }

        [Theory]
        [InlineData(2022, 1, 25, 26)]
        [InlineData(2022, 2, 22, 23)]
        [InlineData(2022, 3, 29, 30)]
        [InlineData(2022, 5, 25, 31)]
        public void GetDaysOfMonth_LastWeekDayOfMonth(int year, int month, params int[] expectedDays) {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new HashSet<(DayOfWeekInMonth, DayOfWeek)>() { 
                    (DayOfWeekInMonth.First, DayOfWeek.Monday),
                    (DayOfWeekInMonth.Second, DayOfWeek.Monday),
                    (DayOfWeekInMonth.Third, DayOfWeek.Monday),
                    (DayOfWeekInMonth.Fourth, DayOfWeek.Monday),
                    (DayOfWeekInMonth.Last, DayOfWeek.Tuesday),
                    (DayOfWeekInMonth.Last, DayOfWeek.Wednesday)
                }
            };

            var result = pattern.GetDaysOfMonth(new DateTime(year, month, 1));

            Assert.Equal(expectedDays.ToHashSet(), result);
        }

        [Theory]
        [InlineData(1, "2022-01-01", "2022-01-01", 0, 0)]
        [InlineData(1, "2022-01-01", "2022-02-02", 0, 1)]
        [InlineData(3, "2022-01-01", "2022-02-15", 1, 14)]
        [InlineData(3, "2022-01-01", "2022-05-15", 1, 14)]
        [InlineData(3, "2022-01-15", "2022-01-15", 0, 14)]
        [InlineData(3, "2022-01-15", "2022-03-14", 2, 13)]
        public void GetCurrentDay(int interval, DateTime start, DateTime current, int expectedMonth, int expectedDay) {
            var pattern = new MonthlyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
            };

            var result = pattern.GetCurrentDay(current);

            Assert.Equal(new DateSpan(expectedMonth, expectedDay), result);
        }

        [Theory]
        [InlineData(1, "2022-01-03", "2022-01-01", false, 0, 5, 5, 25)]
        [InlineData(1, "2022-01-03", "2022-01-06", false, 0, 20, 5, 25)]
        [InlineData(1, "2022-01-03", "2022-01-06", true, 0, 0, 5, 25)]
        [InlineData(3, "2022-01-03", "2022-02-01", false, 2, 5, 5, 25)]

        // TODO fix month overflow test cases
        [InlineData(1, "2022-02-01", "2022-02-01", false, 1, 30, 30)]
        [InlineData(3, "2022-01-01", "2022-01-01", false, 6, 30, 30)]

        // TODO more tests
        public void GetDateSpanUntilNextDay_Only_DaysOfMonth(int interval, DateTime start, DateTime current, bool allowCurrent, int expectedMonth, int expectedDay, params int[] daysOfMonth) {
            var pattern = new MonthlyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
                DaysOfMonth = new HashSet<int>(daysOfMonth)
            };

            var result = pattern.GetDateSpanUntilNextDay(current, allowCurrent);

            Assert.Equal(new DateSpan(expectedMonth, expectedDay), result);
        }
    }
}
