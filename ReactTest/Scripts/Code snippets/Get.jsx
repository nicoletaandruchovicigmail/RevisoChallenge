   var App = React.createClass({
    getInitialState: function () {
        return { data: [] };
    },

    componentWillMount: function () {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.url, true);
        xhr.onload = function () {
            var webAPIData = JSON.parse(xhr.responseText);
            this.setState({ data: webAPIData });
        }.bind(this);
        xhr.send();
    },

    render: function () {
        return (

            <tr>
            <td>{ this.state.data[0].Name } </td>
            <td> { this.state.data[0].Description } </td>
            <td> { this.state.data[0].Start } </td>
            </tr>
        );
    }
});

ReactDOM.render(
    <App url="http://localhost:53438/api/values/" />,
    document.getElementById("project"));