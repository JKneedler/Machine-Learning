using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Matrix {
	public int rows;
	public int columns;
	public float[][] matrix;

	public Matrix(int rows, int columns){
		this.rows = rows;
		this.columns = columns;
		this.matrix = new float[rows][];
		for(int i = 0; i < rows; i++){
			this.matrix[i] = new float[columns];
		}
	}

	public Matrix(int rows, int columns, float[] array){
		this.rows = rows;
		this.columns = columns;
		this.matrix = new float[rows][];
		for(int i = 0; i < (array.Length / columns); i++){
			this.matrix[i] = new float[columns];
			for(int j = 0; j < (array.Length / rows); j++){
				matrix[i][j] = array[(i * columns) + j];
			}
		}
	}

	public void Randomize(float min, float max){
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < columns; j++){
				matrix[i][j] = Random.Range(min, max);
			}
		}
	}

	public void ScalarMultiply(float multiplier){
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < columns; j++){
				matrix[i][j] *= multiplier;
			}
		}
	}

	public void ScalarAdd(float adder){
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < columns; j++){
				matrix[i][j] += adder;
			}
		}
	}

	//requires the Matrix to be the same width and height
	public void ElementAdd(Matrix adder){
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < columns; j++){
				matrix[i][j] += adder.matrix[i][j];
			}
		}
	}

	//requires the Matrix to be the same width and height
	public void ElementMultiply(Matrix multiplier){
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < columns; j++){
				matrix[i][j] *= multiplier.matrix[i][j];
			}
		}
	}

	public static Matrix ElementSubtract(Matrix a, Matrix b){
		Matrix c = new Matrix(a.rows, a.columns);
		for(int i = 0; i < c.rows; i++){
			for(int j = 0; j < c.columns; j++){
				c.matrix[i][j] = a.matrix[i][j] - b.matrix[i][j];
			}
		}
		return c;
	}

	public static Matrix DotProduct(Matrix a, Matrix b){
		if(a.columns != b.rows) Debug.LogError("Matrices not compatible in dimensions");
		Matrix c = new Matrix(a.rows, b.columns);
		for(int i = 0; i < c.rows; i++){
			for(int j = 0; j < c.columns; j++){
				float sum = 0;
				for(int k = 0; k < a.columns; k++){
					sum  += a.matrix[i][k] * b.matrix[k][j];
				}
				c.matrix[i][j] = sum;
			}
		}
		return c;
	}

	public static Matrix Transpose(Matrix a){
		Matrix b = new Matrix(a.columns, a.rows);
		for(int i = 0; i < a.rows; i++){
			for(int j = 0; j< a.columns; j++){
				b.matrix[j][i] = a.matrix[i][j];
			}
		}
		return b;
	}

	public void Map(System.Func<float, float> fun){
		for(int i = 0; i < rows; i++){
			for(int j = 0; j< columns; j++){
				float val = matrix[i][j];
				matrix[i][j] = fun(val);
			}
		}
	}

	public static Matrix MapStatic(Matrix m, System.Func<float, float> fun){
		Matrix newM = new Matrix(m.rows, m.columns);
		for(int i = 0; i < m.rows; i++){
			for(int j = 0; j< m.columns; j++){
				float val = m.matrix[i][j];
				newM.matrix[i][j] = fun(val);
			}
		}
		return newM;
	}

	public static Matrix FromArray(float[] nums){
		Matrix arrayM = new Matrix(nums.Length, 1);
		for(int i = 0; i < nums.Length; i++){
			arrayM.matrix[i][0] = nums[i];
		}
		return arrayM;
	}

	public float[] ToArray(){
		float[] ar = new float[rows];
		for(int i = 0; i < rows; i++){
			ar[i] = matrix[i][0];
		}
		return ar;
	}

	public static Matrix MixedMatrix(Matrix a, Matrix b, float mutationRate, float min, float max){
		Matrix c = new Matrix(a.columns, a.rows);
		for(int i = 0; i < c.rows; i++){
			for(int j = 0; j< c.columns; j++){
				c.matrix[i][j] = Matrix.RandomOfTwo(a.matrix[i][j], b.matrix[i][j], mutationRate, min, max);
			}
		}
		return c;
	}

	public static float RandomOfTwo(float a, float b, float mutationRate, float min, float max){
		if(Random.Range(0f, 1f) < mutationRate){
			return Random.Range(min, max);
		} else {
			if(Random.Range(0f, 1f) > .5f){
				return a;
			} else {
				return b;
			}
		}
	}

	public override string ToString(){
		string ms = "{";
		for(int i = 0; i < rows; i++){
			if(i != 0) ms += ", ";
			ms += "(";
			for(int j = 0; j < columns; j++){
				if(j != 0) ms += ", ";
				ms += matrix[i][j] + "";
			}
			ms += ")";
			if(i != rows-1) ms += "\n";
		}
		ms += "}";
		ms = ms.Replace("\n", System.Environment.NewLine);
		return ms;
	}
}
