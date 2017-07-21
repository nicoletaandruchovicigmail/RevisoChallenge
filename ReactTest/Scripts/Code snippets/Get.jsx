var App = React.createClass({
    getInitialState: function() {
        return { data: [] };
    },

    componentWillMount: function() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.url, true);
        xhr.onload = function() {
            var webAPIData = JSON.parse(xhr.responseText);
            this.setState({ data: webAPIData });
        }.bind(this);
        xhr.send();
    },

    render: function() {
        return (
            <div>
                <h2>Name: { this.state.data.Name } </h2>
                <h2> Desc: { this.state.data.Description } </h2>
                <h2> Start: { this.state.data.Start } </h2>
            </div>
        );
    }
});

ReactDOM.render(
    <App url="http://localhost:53438/api/values/3"/>,
    document.getElementById("container"));