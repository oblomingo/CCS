import React, {Component} from 'react';
import PropTypes from 'prop-types';
import {connect} from 'react-redux';
import {dashboardDataFetchData} from '../../actions/dashboardData';

class Dashboard extends Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
    this.props.fetchData('/api/Dashboard');
  }

  render() {
    if (this.props.hasErrored) {
      return <p>Sorry! There was an error loading the items</p>;
    }

    if (this.props.isLoading) {
      return <p>Loading…</p>;
    }

    return (
      <div className="Dashboard">
        <h1>Dashboard</h1>
        <div>Sensors & Relays</div>
        <div>Temperature:{this.props.dashboardData.temperature}</div>
        <div>Humidity:{this.props.dashboardData.humidity}</div>
        <div>Is Relay On:{String(this.props.dashboardData.isOn)}</div>
        <br/>
        <div>Settings:</div>
        <div>Mode:{this.props.dashboardData.settings.mode}</div>
        <div>Inner Temperature Maximum:{this.props.dashboardData.settings.innerTemperatureMax}</div>
        <div>Inner Temperature Minimum:{this.props.dashboardData.settings.innerTemperatureMin}</div>
        <div>Outer Temperature Maximum:{this.props.dashboardData.settings.outerTemperatureMax}</div>
        <div>Outer Temperature Minimum:{this.props.dashboardData.settings.outerTemperatureMax}</div>
        <div>Schedule Turn On:{this.props.dashboardData.settings.scheduleStar}</div>
        <div>Schedule Turn Off:{this.props.dashboardData.settings.scheduleStop}</div>
        <div>Manual Mode Is On:{String(this.props.dashboardData.settings.IsOn)}</div>
      </div>
    );
  }
}

Dashboard.propTypes = {
  fetchData: PropTypes.func.isRequired,
  dashboardData: PropTypes.object.isRequired,
  hasErrored: PropTypes.bool,
  isLoading: PropTypes.bool
};

const mapStateToProps = (state) => {
  return {
      dashboardData: state.dashboardDataFetchDataSuccess,
      hasErrored: state.dashboardDataHasErrored,
      isLoading: state.dashboardDataIsLoading
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
      fetchData: (url) => dispatch(dashboardDataFetchData(url))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Dashboard);