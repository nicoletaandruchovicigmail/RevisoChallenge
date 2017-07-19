var Comment = React.createClass({
    edit: function() {
        alert("Editing comment");
    },
    remove: function() {
        alert("Removing comment");
    },
    render: function() {
        return (
            <div className="commentContainer">
                <div className="comment"> { this.props.children } </div>
                <button onClick={ this.edit } className="button-primary"> Edit </button>
                <button onClick={ this.remove } className="button-danger"> Remove </button>
            </div>
        );
    }
});

ReactDOM.render(
    <div className="board">
        <Comment>Comment1</Comment>
        <Comment>Comment2</Comment>
        <Comment>Comment3</Comment>
    </div>, document.getElementById('content'));