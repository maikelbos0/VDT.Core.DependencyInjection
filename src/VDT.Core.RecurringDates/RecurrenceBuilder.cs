﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Builder for date recurrences
    /// </summary>
    public class RecurrenceBuilder : IRecurrenceBuilder {
        /// <summary>
        /// Gets or sets the inclusive start date for a recurrence; defaults to <see cref="DateTime.MinValue"/>
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the inclusive end date for a recurrence; defaults to <see cref="DateTime.MaxValue"/>
        /// </summary>
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        /// <summary>
        /// Gets or sets the maximum number of occurrences for a recurrence; if <see langword="null"/> it repeats without limit
        /// </summary>
        public int? Occurrences { get; set; }

        /// <summary>
        /// Gets or sets the builders which will be invoked to build recurrence patterns for this recurrence
        /// </summary>
        public List<RecurrencePatternBuilder> PatternBuilders { get; set; } = new();

        /// <inheritdoc/>
        public IRecurrenceBuilder From(DateTime startDate) {
            StartDate = startDate.Date;
            return this;
        }

        /// <inheritdoc/>
        public IRecurrenceBuilder Until(DateTime endDate) {
            EndDate = endDate.Date;
            return this;
        }

        /// <inheritdoc/>
        public IRecurrenceBuilder StopAfter(int occurrences) {
            Occurrences = occurrences;
            return this;
        }

        /// <inheritdoc/>
        public DailyRecurrencePatternBuilder Daily() {
            var builder = new DailyRecurrencePatternBuilder(this, 1);
            PatternBuilders.Add(builder);
            return builder;
        }

        /// <inheritdoc/>
        public WeeklyRecurrencePatternBuilder Weekly() {
            var builder = new WeeklyRecurrencePatternBuilder(this, 1);
            PatternBuilders.Add(builder);
            return builder;
        }

        /// <inheritdoc/>
        public MonthlyRecurrencePatternBuilder Monthly() {
            var builder = new MonthlyRecurrencePatternBuilder(this, 1);
            PatternBuilders.Add(builder);
            return builder;
        }

        /// <inheritdoc/>
        public RecurrencePatternBuilderStart Every(int interval) {
            return new RecurrencePatternBuilderStart(this, interval);
        }

        /// <inheritdoc/>
        public Recurrence Build() {
            return new Recurrence(StartDate, EndDate, Occurrences, PatternBuilders.Select(builder => builder.BuildPattern()));
        }
    }
}