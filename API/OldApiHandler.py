from flask import Flask, jsonify, request
from flask_cors import CORS
import sqlite3

from dotenv import load_dotenv
import os

app = Flask(__name__)
CORS(app)


@app.route('/shopping-list', methods=['GET'])
def get_shopping_list():
    try:
        con = sqlite3.connect("shoppingList.db")
        cursor = con.cursor()

        cursor.execute("""
        SELECT * FROM shoppingList
        """)
        result = cursor.fetchall()
        con.close()

        list_result = []

        for row in result:
            list_result.append(
                {
                    "id": row[0],
                    "name": row[1],
                    "count": row[2]
                }
            )
        return list_result, 200

    except Exception as e:
        if con:
            con.rollback()
            con.close()
            return jsonify({'message': str(e)}), 500


@app.route('/shopping-list', methods=['POST'])
def post_shopping_list():
    try:

        data = request.json
        name = data.get('name')
        count = int(data.get('count'))

        if not name or count is None:
            return jsonify({'message': 'Name and count are required fields.'}), 400

        con = sqlite3.connect("shoppingList.db")
        cursor = con.cursor()

        cursor.execute("""
        INSERT INTO shoppingList (name, count) 
        VALUES (?,?)
        """, (name, count))

        con.commit()

        cursor.execute("""
        SELECT * FROM shoppingList
        """)
        result = cursor.fetchall()
        con.close()

        list_result = []

        for row in result:
            list_result.append(
                {
                    "id": row[0],
                    "name": row[1],
                    "count": row[2]
                }
            )
        return list_result, 201

    except Exception as e:
        if con:
            con.rollback()
            con.close()
            return jsonify({'message': str(e)}), 500


@app.route('/shopping-list', methods=['DELETE'])
def delete_shopping_list():
    try:
        id = request.args.get('id')

        if not id:
            return jsonify({'message': 'id is required'}), 400

        con = sqlite3.connect("shoppingList.db")
        cursor = con.cursor()

        id = int(id)
        cursor.execute("""
        DELETE FROM shoppingList WHERE id = ?
        """, (id,))
        con.commit()

        cursor.execute("""
        SELECT * FROM shoppingList
        """)
        result = cursor.fetchall()

        list_result = []

        for row in result:
            list_result.append(
                {
                    "id": row[0],
                    "name": row[1],
                    "count": row[2]
                }
            )
        return list_result, 200

    except Exception as e:
        if con:
            con.rollback()
            con.close()
            return jsonify({'message': str(e)}), 500


# const response = await this.instance.put('/shopping-list', { id: id, add: addedValue });
@app.route('/shopping-list', methods=['PUT'])
def update_shopping_list_item():
    try:
        data = request.get_json()
        print(data)
        id = int(data.get('id'))
        added = int(data.get('added'))

        # TODO assert type
        if id is None or added is None:
            return jsonify({'message': 'Id and added are required fields.'}), 400

        con = sqlite3.connect("shoppingList.db")
        cursor = con.cursor()

        cursor.execute("""
        UPDATE shoppingList set count = count + ? WHERE id = ?
        """, (added, id))

        con.commit()

        cursor.execute("""
        SELECT * FROM shoppingList
        """)
        result = cursor.fetchall()

        list_result = []

        for row in result:
            list_result.append(
                {
                    "id": row[0],
                    "name": row[1],
                    "count": row[2]
                }
            )
        return list_result, 201

    except Exception as e:
        if con:
            con.rollback()
            con.close()
            return jsonify({'message': str(e)}), 500


if __name__ == '__main__':
    path = os.path.join("..", ".env")
    load_dotenv(dotenv_path=path)

    DB_NAME = os.getenv("DB_NAME")
    API_PORT = int(os.getenv("API_PORT"))

    con = sqlite3.connect(DB_NAME)
    cursor = con.cursor()

    cursor.execute("""
    CREATE TABLE IF NOT EXISTS shoppingList (
                   id INTEGER PRIMARY KEY,
                   name TEXT NOT NULL, 
                   count INTEGER DEFAULT 0
    )
""")

    app.run(port=API_PORT)
