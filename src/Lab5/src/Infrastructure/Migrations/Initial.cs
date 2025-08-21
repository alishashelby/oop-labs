using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;
using System;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return """
               create type transaction_type as enum
               (
                   'deposit',
                   'withdraw'
               );

               create table account_balance
               (
                   account_id int not null primary key,
                   pin varchar not null,
                   balance decimal not null
               );

               create table transactions
               (
                   transaction_id uuid primary key,
                   account_id int not null references account_balance(account_id),
                   amount decimal not null,
                   type transaction_type not null,
                   date_time timestamp not null
               );
               """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return """
               drop table account_balance if exists account_balance;
               drop table transactions if exists transactions;
               drop type transaction_type;
               """;
    }
}