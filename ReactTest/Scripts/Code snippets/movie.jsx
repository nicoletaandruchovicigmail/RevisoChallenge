var Movie = React.createClass({
    render: function() {
        return (
            <div>
                <h1>{ this.props.title } </h1>
                <h2> { this.props.genre }</h2>
            </div>
        );
    }
});

ReactDOM.render(
    <div>
        <Movie title="Avatar" genre="action"/>
        <Movie title="The Notebook" genre="romance"/>
        <Movie title="Captain America" genre="action"/>
    </div>,
    document.getElementById("content"));;