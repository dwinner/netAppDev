// Copyright (C) 2011 Oliver Sturm <oliver@oliversturm.com>
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, see <http://www.gnu.org/licenses/>.
  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.IO;

namespace chapter20 {
  class Program {
    private const string DBCONNSTR = @"Data Source=database.sdf;Persist Security Info=False;";
    private const string DBFILE = "database.sdf";

    static void Main(string[] args) {
      // Init, just for the demo
      if (File.Exists(DBFILE))
        File.Delete(DBFILE);

      CreateDatabase( );
      FillDatabase4( );
    }

    static void CreateDatabase( ) {
      var engine = new SqlCeEngine(DBCONNSTR);
      engine.CreateDatabase( );
    }

    static void FillDatabase( ) {
      using(var conn = new SqlCeConnection(DBCONNSTR)) {
        conn.Open( );
        try {
          using (var trans = conn.BeginTransaction( )) {
            ExecuteSQL(trans, "create table people(id int, name ntext)");
            trans.Commit( );
          }

          using (var trans = conn.BeginTransaction( )) {
            ExecuteSQL(trans, "insert into people(id, name) values(1, 'Harry')");
            ExecuteSQL(trans, "insert into people(id, name) values(2, 'Jane')");
            ExecuteSQL(trans, "insert into people(id, name) values(3, 'Willy')");
            ExecuteSQL(trans, "insert into people(id, name) values(4, 'Susan')");
            ExecuteSQL(trans, "insert into people(id, name) values(5, 'Bill')");
            ExecuteSQL(trans, "insert into people(id, name) values(6, 'Jennifer')");
            ExecuteSQL(trans, "insert into people(id, name) values(7, 'John')");
            ExecuteSQL(trans, "insert into people(id, name) values(8, 'Anna')");
            ExecuteSQL(trans, "insert into people(id, name) values(9, 'Bob')");
            ExecuteSQL(trans, "insert into people(id, name) values(10, 'Mary')");

            trans.Commit( );
          }
        }
        finally {
          conn.Close( );
        }
      }
    }

    static void FillDatabase2( ) {
      using (var conn = new SqlCeConnection(DBCONNSTR)) {
        conn.Open( );
        try {
          using (var trans = conn.BeginTransaction( )) {
            ExecuteSQL(trans, "create table people(id int, name ntext)");
            trans.Commit( );
          }

          using (var trans = conn.BeginTransaction( )) {
            Action<SqlCeTransaction, int, string> exec = (transaction, id, name) =>
              ExecuteSQL(transaction, String.Format("insert into people(id, name) values({0}, '{1}')", id, name));
            exec(trans, 1, "Harry");
            exec(trans, 2, "Jane");
            exec(trans, 3, "Willy");
            exec(trans, 4, "Susan");
            exec(trans, 5, "Bill");
            exec(trans, 6, "Jennifer");
            exec(trans, 7, "John");
            exec(trans, 8, "Anna");
            exec(trans, 9, "Bob");
            exec(trans, 10, "Mary");

            trans.Commit( );
          }
        }
        finally {
          conn.Close( );
        }
      }
    }

    static void FillDatabase3( ) {
      using (var conn = new SqlCeConnection(DBCONNSTR)) {
        conn.Open( );
        try {
          using (var trans = conn.BeginTransaction( )) {
            ExecuteSQL(trans, "create table people(id int, name ntext)");
            trans.Commit( );
          }

          using (var trans = conn.BeginTransaction( )) {
            var exec = (
              (Func<SqlCeTransaction, Func<int, Action<string>>>)
              (transaction => id => name =>
                ExecuteSQL(transaction, String.Format("insert into people(id, name) values({0}, '{1}')", id, name))))
                (trans);

            exec(1)("Harry");
            exec(2)("Jane");
            exec(3)("Willy");
            exec(4)("Susan");
            exec(5)("Bill");
            exec(6)("Jennifer");
            exec(7)("John");
            exec(8)("Anna");
            exec(9)("Bob");
            exec(10)("Mary");

            trans.Commit( );
          }
        }
        finally {
          conn.Close( );
        }
      }
    }

    static void FillDatabase4( ) {
      using (var conn = new SqlCeConnection(DBCONNSTR)) {
        conn.Open( );
        try {
          using (var trans = conn.BeginTransaction( )) {
            ExecuteSQL(trans, "create table people(id int, name ntext)");
            trans.Commit( );
          }

          using (var trans = conn.BeginTransaction( )) {
            Action<int, string> exec = (id, name) =>
                ExecuteSQL(trans, String.Format("insert into people(id, name) values({0}, '{1}')", id, name));

            exec(1, "Harry");
            exec(2, "Jane");
            exec(3, "Willy");
            exec(4, "Susan");
            exec(5, "Bill");
            exec(6, "Jennifer");
            exec(7, "John");
            exec(8, "Anna");
            exec(9, "Bob");
            exec(10, "Mary");

            trans.Commit( );
          }
        }
        finally {
          conn.Close( );
        }
      }
    }


    static void ExecuteSQL(SqlCeTransaction transaction, string sql) {
      var command = transaction.Connection.CreateCommand( );
      command.Transaction = transaction;
      command.CommandText = sql;
      command.ExecuteNonQuery( );
    }

  }
}
