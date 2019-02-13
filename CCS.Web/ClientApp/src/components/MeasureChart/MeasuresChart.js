import React, {Component} from 'react';
import PropTypes from 'prop-types';
import {connect} from 'react-redux';
import ResponsiveContainer from 'recharts/lib/component/ResponsiveContainer';
import {chartDataFetchData} from '../../actions/ChartData';
import LineChart from 'recharts/lib/chart/LineChart';
import ComposedChart from 'recharts/lib/chart/ComposedChart';
import Line from 'recharts/lib/cartesian/Line';
import Bar from 'recharts/lib/cartesian/Bar';
import XAxis from 'recharts/lib/cartesian/XAxis';
import YAxis from 'recharts/lib/cartesian/YAxis';
import CartesianGrid from 'recharts/lib/cartesian/CartesianGrid';
import Tooltip from 'recharts/lib/component/Tooltip';
import Legend from 'recharts/lib/component/Legend';

class MeasuresChart extends Component {
  
	componentDidMount() {
	  this.props.fetchData('/api/Measures');
	}

	render() {
	  if (this.props.hasErrored) {
		return <p>Sorry! There was an error loading the items</p>;
	  }
  
	  if (this.props.isLoading) {
		return <p>Loading…</p>;
	  }

	  return (
		// 99% per https://github.com/recharts/recharts/issues/172
		<ResponsiveContainer width="99%" height={320}>
		<ComposedChart width={800} height={300} data={this.props.chartData} margin={{top: 20, right: 0, bottom: 0, left: 0}} syncId="anyId">
		<XAxis dataKey="time"/>
		<YAxis yAxisId="left" orientation="left" stroke="#8884d8"/>
		<YAxis yAxisId="right" orientation="right" stroke="#82ca9d"/>
		<CartesianGrid strokeDasharray="6 6"/>
		<Tooltip/>
		<Legend />
			<Bar yAxisId="right" dataKey='isOn' barSize={20} fill='#413ea0'/>
		<Line type="monotone" yAxisId="left" dataKey="humidity" stroke="#8884d8" />
		<Line type="monotone" yAxisId="right" dataKey="temperature" stroke="#82ca9d" />
		</ComposedChart>
		</ResponsiveContainer>
	  );
	}
  }

MeasuresChart.propTypes = {
	fetchData: PropTypes.func.isRequired,
	chartData: PropTypes.array.isRequired,
	hasErrored: PropTypes.bool,
	isLoading: PropTypes.bool
  };
  
const mapStateToProps = (state) => {
	return {
		chartData: state.chartDataFetchDataSuccess, 
		hasErrored: state.chartDataHasErrored, 
		isLoading: state.chartDataIsLoading
	};
};

const mapDispatchToProps = (dispatch) => {
	return {
		fetchData: (url) => dispatch(chartDataFetchData(url))
	};
};
  
 export default connect(mapStateToProps, mapDispatchToProps)(MeasuresChart);
