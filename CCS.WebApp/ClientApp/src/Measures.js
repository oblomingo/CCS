import React, { Component } from 'react';
import PropTypes from 'prop-types';
//import ResponsiveContainer from 'recharts/lib/component/ResponsiveContainer';
//import LineChart from 'recharts/lib/chart/LineChart';
//import ComposedChart from 'recharts/lib/chart/ComposedChart';
//import Line from 'recharts/lib/cartesian/Line';
//import Bar from 'recharts/lib/cartesian/Bar';
//import XAxis from 'recharts/lib/cartesian/XAxis';
//import YAxis from 'recharts/lib/cartesian/YAxis';
//import CartesianGrid from 'recharts/lib/cartesian/CartesianGrid';
//import Tooltip from 'recharts/lib/component/Tooltip';
//import Legend from 'recharts/lib/component/Legend';
import {
	ResponsiveContainer, ComposedChart, Line, Bar, Area, Scatter, XAxis,
	YAxis, ReferenceLine, ReferenceDot, Tooltip, Legend, CartesianGrid, Brush,
	LineChart
} from 'recharts';

export class Measures extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
			chartData: [],
			hasErrored: false,
			isLoading: false
        };
    }

    componentDidMount() {
        this.getChartData();
    }

    render() {
		if (this.state.hasErrored) {
			return <p>Sorry! There was an error loading the items</p>;
		}

		if (this.state.isLoading) {
			return <p>Loading…</p>;
		}

		return (
			<ResponsiveContainer width="99%" height={320}>
				<ComposedChart width={800} height={300} data={this.state.chartData} margin={{ top: 20, right: 0, bottom: 0, left: 0 }} syncId="anyId">
					<XAxis dataKey="time" />
					<YAxis yAxisId="left" orientation="left" stroke="#8884d8" />
					<YAxis yAxisId="right" orientation="right" stroke="#82ca9d" />
					<CartesianGrid strokeDasharray="6 6" />
					<Tooltip />
					<Legend />
					<Bar yAxisId="right" dataKey='isOn' barSize={20} fill='#413ea0' />
					<Line type="monotone" yAxisId="left" dataKey="humidity" stroke="#8884d8" />
					<Line type="monotone" yAxisId="right" dataKey="temperature" stroke="#82ca9d" />
				</ComposedChart>
			</ResponsiveContainer>
		);
    }

    async getChartData() {
        this.setState({ ...this.state, loading: true });
        const response = await fetch('api/Measures');
        const chartData = await response.json();
        this.setState({ ...this.state, chartData: chartData, loading: false });
    }
}
