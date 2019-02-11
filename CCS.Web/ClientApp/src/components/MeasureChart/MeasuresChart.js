import React from 'react';
import ResponsiveContainer from 'recharts/lib/component/ResponsiveContainer';
import LineChart from 'recharts/lib/chart/LineChart';
import ComposedChart from 'recharts/lib/chart/ComposedChart';
import Line from 'recharts/lib/cartesian/Line';
import Bar from 'recharts/lib/cartesian/Bar';
import XAxis from 'recharts/lib/cartesian/XAxis';
import YAxis from 'recharts/lib/cartesian/YAxis';
import CartesianGrid from 'recharts/lib/cartesian/CartesianGrid';
import Tooltip from 'recharts/lib/component/Tooltip';
import Legend from 'recharts/lib/component/Legend';


const data = [
	{time: '10:30', temperature: 2, humidity: 23, isOn: 1},
	{time: '11:00', temperature: 3, humidity: 24, isOn: 1},
	{time: '11:30', temperature: 3, humidity: 25, isOn: 0},
	{time: '12:00', temperature: 4, humidity: 30, isOn: 1},
	{time: '12:30', temperature: 3, humidity: 25, isOn: 0},
	{time: '13:00', temperature: 2, humidity: 25, isOn: 1},
	{time: '13:30', temperature: 4, humidity: 26, isOn: 0},
];

function MeasuresChart() {
  return (
    // 99% per https://github.com/recharts/recharts/issues/172
    <ResponsiveContainer width="99%" height={320}>
      <ComposedChart width={600} height={300} data={data} margin={{top: 20, right: 0, bottom: 0, left: 0}} syncId="anyId">
       <XAxis dataKey="time"/>
       <YAxis yAxisId="left" orientation="left" stroke="#8884d8"/>
       <YAxis yAxisId="right" orientation="right" stroke="#82ca9d"/>
       <CartesianGrid strokeDasharray="6 6"/>
       <Tooltip/>
       <Legend />
    	<Bar yAxisId="right" dataKey='isOn' barSize={40} fill='#413ea0'/>
       <Line type="monotone" yAxisId="left" dataKey="humidity" stroke="#8884d8" />
       <Line type="monotone" yAxisId="right" dataKey="temperature" stroke="#82ca9d" />
      </ComposedChart>
    </ResponsiveContainer>
  );
}

export default MeasuresChart;